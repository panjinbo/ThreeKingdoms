using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class Controller
{
    //获取模型
    protected T GetModel<T>()
        where T:Model
    {
        return MVC.GetModel<T>() as T;
    }

    //获取视图
    protected T GetView<T>()
        where T : View
    {
        return MVC.GetView<T>() as T;
    }

    protected void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }

    protected void RegisterView(View view)
    {
        MVC.RegisterView(view);
    }

    protected void RegisterController(string eventName,Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    //处理系统消息    
    public abstract void Execute(object data);
}