﻿using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using MyToDoApi.Context;
using System.Reflection;

namespace UserMgrWebApi
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var result = await next();
            if (result.Exception != null)
            {
                return;
            }

            //MethodInfo是ControllerActionDescriptor独有的，所有需要显式转换
            var actionDesc = context.ActionDescriptor as ControllerActionDescriptor;
            if (actionDesc == null)
            {
                return;
            }
            //获取方法上的attribute，是否有UnitOfWorkAttribute
            var uowAttr = actionDesc.ControllerTypeInfo.GetCustomAttribute<UnitOfWorkAttribute>();
            if (uowAttr == null)
            {
                return;
            }
            foreach (var dbCtxType in uowAttr.DbConextTypes)
            {
                //通过DI获得Dbcontext的实例
                var dbCtx = context.HttpContext.RequestServices.GetService(dbCtxType) as MyToDoDBContext;
                if (dbCtx != null)
                {
                    await dbCtx.SaveChangesAsync();
                }
            }
        }
    }
}
