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
    public partial class Plus : System.Web.UI.Page
    {
        MarcaTableAdapter marcaTable = new MarcaTableAdapter();
        TipoTableAdapter tipoTable = new TipoTableAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                list1.Visible = false;
                list2.Visible = false;
                ListBrand();
                ListType();
            }
        }

        void ListBrand()
        {
            list1.Visible = false;
            GridView1.DataSource = marcaTable.ListBrand();
            GridView1.DataBind();
        }
        void ListBrandSelected()
        {
            GridView1.DataSource = marcaTable.FindABrand(int.Parse(search1.Text));
            GridView1.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ListBrand();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            if (list1.Visible.Equals(false))
                ListBrand();
            else ListBrandSelected();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            if (list1.Visible.Equals(false))
                ListBrand();
            else ListBrandSelected();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
                string name = ((TextBox)GridView1.Rows[e.RowIndex].FindControl(("name"))).Text;

                marcaTable.UpdateABrand(name, ID);
                GridView1.EditIndex = -1;
                if (list1.Visible.Equals(false))
                    ListBrand();
                else ListBrandSelected();
            }
            catch(Exception ex) { }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    string name = ((TextBox)GridView1.FooterRow.FindControl(("nameFooter"))).Text;
                    if (name != String.Empty)
                    {
                        marcaTable.AddANewBrand(name);
                        ListBrand();
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "showPopup();", true);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSearch1_Click(object sender, EventArgs e)
        {
            if (search1.Text != String.Empty)
            {
                list1.Visible = true;
                ListBrandSelected();
            }
        }

        protected void list1_Click(object sender, EventArgs e)
        {
            list1.Visible = false;
            search1.Text = String.Empty;
            ListBrand();
        }


        void ListType()
        {
            GridView2.DataSource = tipoTable.ListCategory();
            GridView2.DataBind();
        }
        void ListTypeSelected()
        {
            GridView2.DataSource = tipoTable.FindACategory(int.Parse(search2.Text));
            GridView2.DataBind();
        }
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            ListType();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    string name = ((TextBox)GridView2.FooterRow.FindControl(("nameFooter"))).Text;
                    if (name != String.Empty)
                    {
                        tipoTable.AddANewCategory(name);
                        ListType();
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "showPopup();", true);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView2.EditIndex = e.NewEditIndex;
            if (list2.Visible.Equals(false))
                ListType();
            else ListTypeSelected();
        }

        protected void GridView2_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView2.EditIndex = -1;
            if (list2.Visible.Equals(false))
                ListType();
            else ListTypeSelected();
        }

        protected void btnSearch2_Click(object sender, EventArgs e)
        {
            if (search2.Text != String.Empty)
            {
                list2.Visible = true;
                ListTypeSelected();
            }
        }

        protected void list2_Click(object sender, EventArgs e)
        {
            list2.Visible = false;
            search2.Text = String.Empty;
            ListType();
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int ID = int.Parse(GridView2.DataKeys[e.RowIndex].Value.ToString());
                string name = ((TextBox)GridView2.Rows[e.RowIndex].FindControl(("name"))).Text;

                tipoTable.UpdateACategory(name, ID);
                GridView2.EditIndex = -1;
                if (list2.Visible.Equals(false))
                    ListType();
                else ListTypeSelected();
            }
            catch (Exception ex) { }
        }
    }
}