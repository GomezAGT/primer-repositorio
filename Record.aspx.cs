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
    public partial class Record : System.Web.UI.Page
    {
        HistorialTableAdapter historialTable = new HistorialTableAdapter();
        ProductoTableAdapter productoTable = new ProductoTableAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListRecord();
                list.Visible = false;
            }
        }
        void ListRecord()
        {
            list.Visible = false;
            GridView1.DataSource = historialTable.ListRecord(true);
            GridView1.DataBind();
        }
        void ListRecordSelected()
        {
            GridView1.DataSource = historialTable.FindARecord(true, int.Parse(txtSearch.Text));
            GridView1.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    string nit = ((TextBox)GridView1.FooterRow.FindControl(("nitFooter"))).Text;
                    string name = ((TextBox)GridView1.FooterRow.FindControl(("nameFooter"))).Text;
                    int product = int.Parse(((DropDownList)GridView1.FooterRow.FindControl(("productFooter"))).SelectedValue);
                    int amount = int.Parse(((TextBox)GridView1.FooterRow.FindControl(("amountFooter"))).Text);
                    decimal price = decimal.Parse(((TextBox)GridView1.FooterRow.FindControl(("priceFooter"))).Text);
                    decimal subtotal = decimal.Parse(((TextBox)GridView1.FooterRow.FindControl(("subtotalFooter"))).Text);
                    decimal total = decimal.Parse(((TextBox)GridView1.FooterRow.FindControl(("totalFooter"))).Text);

                    historialTable.AddANewRecord(nit, name, product, amount, price, subtotal, total, true);
                    ListRecord();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "showPopup();", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            if (list.Visible.Equals(false))
                ListRecord();
            else ListRecordSelected();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            if (list.Visible.Equals(false))
                ListRecord();
            else ListRecordSelected();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
                string name = ((TextBox)GridView1.Rows[e.RowIndex].FindControl(("name"))).Text;
                int product = int.Parse(((DropDownList)GridView1.Rows[e.RowIndex].FindControl(("product"))).SelectedValue);
                string nit = ((TextBox)GridView1.Rows[e.RowIndex].FindControl(("nit"))).Text;
                int amount = int.Parse(((TextBox)GridView1.Rows[e.RowIndex].FindControl(("amount"))).Text);
                decimal price = decimal.Parse(((TextBox)GridView1.Rows[e.RowIndex].FindControl(("price"))).Text);
                decimal subtotal = decimal.Parse(((TextBox)GridView1.Rows[e.RowIndex].FindControl(("subtotal"))).Text);
                decimal total = decimal.Parse(((TextBox)GridView1.Rows[e.RowIndex].FindControl(("total"))).Text);

                historialTable.UpdateARecord(nit, name, product, amount, price, subtotal, total, ID);
                GridView1.EditIndex = -1;
                if (list.Visible.Equals(false))
                    ListRecord();
                else ListRecordSelected();
            }
            catch(Exception ex) { }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            historialTable.DeleteARecord(false, ID);
            GridView1.EditIndex = -1;
            ListRecord();
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            DropDownList product = GridView1.FooterRow.FindControl("productFooter") as DropDownList;
            product.DataSource = productoTable.LoadProduct(true);
            product.DataTextField = "Nombre";
            product.DataValueField = "ID";
            product.DataBind();

            DataTable dataTable = productoTable.CodeProduct(true, int.Parse(product.SelectedValue));
            TextBox price = GridView1.FooterRow.FindControl("priceFooter") as TextBox;
            price.Text = dataTable.Rows[0][2].ToString();
        }

        protected void product_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList product = GridView1.Rows[GridView1.EditIndex].FindControl("product") as DropDownList;
            int index = int.Parse(product.SelectedValue);
            TextBox price = GridView1.Rows[GridView1.EditIndex].FindControl("price") as TextBox;
            DataTable dataTable = productoTable.CodeProduct(true, index);
            price.Text = dataTable.Rows[0][2].ToString();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    int ID = int.Parse(GridView1.DataKeys[e.Row.RowIndex].Value.ToString());
                    DropDownList product = (DropDownList)e.Row.FindControl("product");
                    product.DataSource = productoTable.LoadProduct(true);
                    product.DataValueField = "ID";
                    product.DataTextField = "Nombre";
                    product.DataBind();

                    DataTable dataTable = historialTable.CodeAproduct(true, ID);
                    product.SelectedValue = dataTable.Rows[0][4].ToString();
                }
            }
        }

        protected void search_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != String.Empty)
            {
                list.Visible = true;
                ListRecordSelected();
            }
        }

        protected void list_Click(object sender, EventArgs e)
        {
            list.Visible = false;
            txtSearch.Text = String.Empty;
            ListRecord();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ListRecord();
        }
    }
}