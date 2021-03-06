﻿using Dapper;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace SatanaServer
{
    public class Bootstrap : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);

            pipelines.BeforeRequest.AddItemToEndOfPipeline(context =>
            {
                Database db = new Database();

                context.Items.Add("db", db);
                db.OpenDatabase();

                return context.Response;
            });

            pipelines.AfterRequest.AddItemToEndOfPipeline(context =>
            {
                context.GetDb()?.CloseDatabase();
            });

            pipelines.OnError += (ctx, e) =>
            {
                App.log.Error($"{e.Message}\r\n{e.StackTrace}");
                return null;
            };

        }
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            pipelines.AfterRequest.AddItemToEndOfPipeline(c =>
            {
                c.Response.Headers["Access-Control-Allow-Origin"] = "*";
                c.Response.Headers["Access-Control-Allow-Methods"] = "POST,GET,PUT,DELETE";
                c.Response.Headers["Access-Control-Allow-Headers"] = "content-type,authorization";
            });
        }
    }
}
