using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Text;

public partial class Pages_Student_Testing123 : System.Web.UI.Page
{
    int i = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

   

  

    protected void btn_Click(object sender, EventArgs e)
    {
       
      
            TextBox1.Text = Convert.ToString(i + 1);
   
      
        i++;
    }
}