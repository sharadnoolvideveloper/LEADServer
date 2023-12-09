<%@ WebService Language="C#" Class="Testing" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Testing  : System.Web.Services.WebService {

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
        
    [WebMethod]
        public string Test()
    {
        return "Hello World";
    }
    [WebMethod]
    public string testing()
    {
        return "pooja";
    }
    [WebMethod]
    public int add(int x, int y)
    {
        return x + y;
    }

    [WebMethod]
    public int sub(int x, int y)
    {
        return x - y;
    }

        [WebMethod]
    public int Mul(int x, int y)
    {
        return x * y;
    }

        [WebMethod]  
     public int Div(int x, int y)  
     {  
         return x / y;  
     }  
}