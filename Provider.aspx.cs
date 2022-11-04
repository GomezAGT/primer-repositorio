using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Tienda.MainDataSetTableAdapters;

namespace Tienda
{
    public partial class Provider : System.Web.UI.Page
    {
        ProviderTableAdapter providerTable = new ProviderTableAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListProvider();
                list.Visible = false;
            }
        }
        void ListProvider()
        {
            list.Visible = false;
            txtSearch.Text = String.Empty;
            GridView1.DataSource = providerTable.ListProvider(true);
            GridView1.DataBind();
        }
        void ListProviderSelected()
        {
            GridView1.DataSource = providerTable.FindAProvider(true, txtSearch.Text);
            GridView1.DataBind();
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            if (list.Visible.Equals(false))
                ListProvider();
            else ListProviderSelected();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            if (list.Visible.Equals(false))
                ListProvider();
            else ListProviderSelected();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    string name = ((TextBox)GridView1.FooterRow.FindControl(("nameFooter"))).Text;
                    string address = ((TextBox)GridView1.FooterRow.FindControl(("addressFooter"))).Text;
                    string phone = ((TextBox)GridView1.FooterRow.FindControl(("phoneFooter"))).Text;

                    providerTable.AddNewProvider(name, address, phone, true);
                    ListProvider();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "showPopup();", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string name = ((TextBox)GridView1.Rows[e.RowIndex].FindControl(("name"))).Text;
            string address = ((TextBox)GridView1.Rows[e.RowIndex].FindControl(("address"))).Text;
            string phone = ((TextBox)GridView1.Rows[e.RowIndex].FindControl(("phone"))).Text;

            providerTable.UpdateAProvider(name, address, phone, ID);
            GridView1.EditIndex = -1;
            if (list.Visible.Equals(false))
                ListProvider();
            else ListProviderSelected();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            providerTable.DeleteAProvider(false, ID);
            GridView1.EditIndex = -1;
            ListProvider();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ListProvider();
        }

        protected void search_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != String.Empty)
            {
                list.Visible = true;
                ListProviderSelected();
            }
        }

        protected void list_Click(object sender, EventArgs e)
        {
            list.Visible = false;
            txtSearch.Text = String.Empty;
            ListProvider();
        }
    }
}