using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tienda
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int form = Convert.ToInt32(Session["form"]);
            if (form.Equals(1))
            {
                product.Attributes.Add("style", "color: #17587E; font-weight: bold;");
                div1.Attributes.Add("style", "background-color: #17587E; height: 6px;");
            }
            else if (form.Equals(2))
            {
                provider.Attributes.Add("style", "color: #17587E; font-weight: bold;");
                div2.Attributes.Add("style", "background-color: #17587E; height: 6px;");
            }
            else if (form.Equals(3))
            {
                record.Attributes.Add("style", "color: #17587E; font-weight: bold;");
                div3.Attributes.Add("style", "background-color: #17587E; height: 6px;");
            }
            else if (form.Equals(4))
            {
                plus.Attributes.Add("style", "color: #17587E; font-weight: bold;");
                div4.Attributes.Add("style", "background-color: #17587E; height: 6px;");
            }
        }

        protected void product_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Add("form", 1);
            Response.Redirect("Product.aspx");
        }

        protected void provider_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Add("form", 2);
            Response.Redirect("Provider.aspx");
        }

        protected void record_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Add("form", 3);
            Response.Redirect("Record.aspx");
        }

        protected void plus_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Add("form", 4);
            Response.Redirect("Plus.aspx");
        }
        protected void exit_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
}