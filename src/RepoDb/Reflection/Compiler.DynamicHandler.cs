﻿using System.Linq.Expressions;
using RepoDb.Interfaces;

namespace RepoDb.Reflection;

partial class Compiler
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbCommandExpression"></param>
    /// <param name="dbHelper"></param>
    /// <returns></returns>
    private static MethodCallExpression GetCompilerDbParameterPostCreationExpression(ParameterExpression dbCommandExpression,
        IDbHelper dbHelper)
    {
        var method = StaticType.IDbHelper.GetMethod(nameof(IDbHelper.DynamicHandler))
            .MakeGenericMethod(dbCommandExpression.Type);
        return Expression.Call(Expression.Constant(dbHelper),
            method, dbCommandExpression, Expression.Constant("RepoDb.private.Compiler.Events[AfterCreateDbParameter]"));
    }
}
