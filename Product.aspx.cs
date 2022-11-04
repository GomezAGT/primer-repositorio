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
    public partial class Product : System.Web.UI.Page
    {
        ProductTableAdapter productTable = new ProductTableAdapter();
        TipoTableAdapter tipoTable = new TipoTableAdapter();
        MarcaTableAdapter marcaTable = new MarcaTableAdapter();
        ProveedorTableAdapter proveedorTable = new ProveedorTableAdapter();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                list.Visible = false;
                ListProduct();
            }
        }
        void ListProduct()
        {
            list.Visible = false;
            txtSearch.Text = String.Empty;
            GridView1.DataSource = productTable.ListProduct(true);
            GridView1.DataBind();
        }
        void ListProductSelected()
        {
            DataTable dataTable = productTable.FindAProduct(true, txtSearch.Text);
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("Add"))
                {
                    string name = ((TextBox)GridView1.FooterRow.FindControl(("nameFooter"))).Text;
                    int brand = int.Parse(((DropDownList)GridView1.FooterRow.FindControl(("brandFooter"))).SelectedValue);
                    Decimal precio = decimal.Parse(((TextBox)GridView1.FooterRow.FindControl(("priceFooter"))).Text);
                    string describe = ((TextBox)GridView1.FooterRow.FindControl(("describeFooter"))).Text;
                    string caducidad = ((TextBox)GridView1.FooterRow.FindControl(("caducidadFooter"))).Text;
                    int type = int.Parse(((DropDownList)GridView1.FooterRow.FindControl(("categoryFooter"))).SelectedValue);
                    int proveedor = int.Parse(((DropDownList)GridView1.FooterRow.FindControl(("providerFooter"))).SelectedValue);

                    productTable.AddNewProduct(name, brand, precio, describe, caducidad, type, proveedor, true);
                    ListProduct();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "showPopup();", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            DropDownList brand = GridView1.FooterRow.FindControl("brandFooter") as DropDownList;
            brand.DataSource = marcaTable.ListBrand();
            brand.DataTextField = "Nombre";
            brand.DataValueField = "ID";
            brand.DataBind();

            DropDownList type = GridView1.FooterRow.FindControl("categoryFooter") as DropDownList;
            type.DataSource = tipoTable.ListCategory();
            type.DataTextField = "Nombre";
            type.DataValueField = "ID";
            type.DataBind();

            DropDownList provider = GridView1.FooterRow.FindControl("providerFooter") as DropDownList;
            provider.DataSource = proveedorTable.ListProvider(true);
            provider.DataTextField = "Nombre";
            provider.DataValueField = "ID";
            provider.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            if (list.Visible.Equals(false))
                ListProduct();
            else ListProductSelected();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            if (list.Visible.Equals(false))
                ListProduct();
            else ListProductSelected();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.Equals(DataControlRowType.DataRow))
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    int id = int.Parse(GridView1.DataKeys[e.Row.RowIndex].Value.ToString());

                    DataTable dataTable = productTable.AllCodeProduct(id);

                    DropDownList brand = (DropDownList)e.Row.FindControl("brand");
                    brand.DataSource = marcaTable.ListBrand();
                    brand.DataValueField = "ID";
                    brand.DataTextField = "Nombre";
                    brand.DataBind();
                    brand.SelectedValue = dataTable.Rows[0][2].ToString();

                    DropDownList type = (DropDownList)e.Row.FindControl("category");
                    type.DataSource = tipoTable.ListCategory();
                    type.DataValueField = "ID";
                    type.DataTextField = "Nombre";
                    type.DataBind();
                    type.SelectedValue = dataTable.Rows[0][6].ToString();

                    DropDownList provider = (DropDownList)e.Row.FindControl("provider");
                    provider.DataSource = proveedorTable.ListProvider(true);
                    provider.DataValueField = "ID";
                    provider.DataTextField = "Nombre";
                    provider.DataBind();
                    provider.SelectedValue = dataTable.Rows[0][7].ToString();

                }
            }
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string name = ((TextBox)GridView1.Rows[e.RowIndex].FindControl(("name"))).Text;
            int brand = int.Parse(((DropDownList)GridView1.Rows[e.RowIndex].FindControl(("brand"))).SelectedValue);
            decimal price = decimal.Parse(((TextBox)GridView1.Rows[e.RowIndex].FindControl(("price"))).Text);
            string describe = ((TextBox)GridView1.Rows[e.RowIndex].FindControl(("describe"))).Text;
            string caducidad = ((TextBox)GridView1.Rows[e.RowIndex].FindControl(("caducidad"))).Text;
            int type = int.Parse(((DropDownList)GridView1.Rows[e.RowIndex].FindControl(("category"))).SelectedValue);
            int provider = int.Parse(((DropDownList)GridView1.Rows[e.RowIndex].FindControl(("provider"))).SelectedValue);

            productTable.UpdateProduct(name, brand, price, describe, caducidad, type, provider, ID);
            GridView1.EditIndex = -1;
            if (list.Visible.Equals(false))
                ListProduct();
            else ListProductSelected();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = int.Parse(GridView1.DataKeys[e.RowIndex].Value.ToString());
            productTable.DeleteAProduct(false, ID);
            GridView1.EditIndex = -1;
            ListProduct();
        }

        protected void search_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != String.Empty)
            {
                list.Visible = true;
                ListProductSelected();
            }

        }

        protected void list_Click(object sender, EventArgs e)
        {
            list.Visible = false;
            txtSearch.Text = String.Empty;
            ListProduct();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            ListProduct();
        }
    }
}