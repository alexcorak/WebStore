using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cart : System.Web.UI.Page
{
    private List<cartProduct> shoppingcart = new List<cartProduct>();
    private SqlDataReader dr = null;
    private int total = 0;
    Table toDisplay = new Table();
    private List<cartProduct> cart2 = new List<cartProduct>();
    protected void Page_Load(object sender, EventArgs e)
    {
        shoppingcart = (List<cartProduct>)Session["MyCart"];
        toDisplay = new Table();
        SqlCommand cmd;
        SqlConnection cn = new SqlConnection();
        cn.ConnectionString = "Data Source=sql7002.site4now.net;" +
    "Initial Catalog=DB_A445E8_salesAdmin;" +
    "MultipleActiveResultSets = true;" +
    "User ID=DB_A445E8_salesAdmin_admin;" +
    "Password=warcraft3";

        header1(CartTable);
        foreach (cartProduct p in shoppingcart)
        {
            cmd = new SqlCommand("select * from Inventory WHERE ItemNumber = '" + p.getItemNumber() + "'", cn);
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cn.Open();
            dr = cmd.ExecuteReader();
            TableRow tableRow = new TableRow();
            TableCell cell = new TableCell();
            CartTable.BackColor = Color.DarkGray;
            CartTable.ForeColor = Color.White;
            while (dr.Read())
            {
                total += int.Parse(dr["UnitPrice"].ToString());
                int name = int.Parse(dr["ItemNumber"].ToString());

                cell.Text = dr["ItemNumber"].ToString();
                tableRow.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dr["UnitPrice"].ToString();
                tableRow.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = dr["Description"].ToString();
                tableRow.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = p.getQuantity().ToString();
                tableRow.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = (p.getQuantity() * int.Parse(dr["UnitPrice"].ToString())).ToString();
                tableRow.Cells.Add(cell);

                Button bt = new Button();
                bt.Text = "Remove From Cart";
                if(name==1)
                    bt.Click += removeCartGPU;
                if (name == 2)
                    bt.Click += removeCartCPU;
                if (name == 3)
                    bt.Click += removeCartMouse;

                tableRow.Cells[4].Controls.Add(bt);

                CartTable.Rows.Add(tableRow);
            }
            footer(TableFooter);
        }
    }

    protected void removeCartExecute(int id)
    {
        shoppingcart = (List<cartProduct>)Session["MyCart"];
        foreach (cartProduct p in cart2)
        {
            
            if(p.getItemNumber()==id)
            {
                p.setQuantity(p.getQuantity() - 1);
                if (p.getQuantity() <= 0)
                {
                    cart2.Remove(p);
                    break;
                }
            }
        }
        Session["MyCart"] = shoppingcart;
    }

    protected void removeCartCPU(object sender, EventArgs e)
    {
        removeCartExecute(2);
    }
    protected void removeCartGPU(object sender, EventArgs e)
    {
        removeCartExecute(1);
    }
    protected void removeCartMouse(object sender, EventArgs e)
    {
        removeCartExecute(3);
    }

    private void header1(Table tbl)
    {
        TableHeaderRow head = new TableHeaderRow();
        head.BackColor = Color.Wheat;
        TableHeaderCell headerCell = new TableHeaderCell();
        headerCell.ForeColor = Color.White;
        headerCell.Text = "Item #: ";
        head.Cells.Add(headerCell);
        headerCell = new TableHeaderCell();
        headerCell.Text = "Item Price: ";
        head.Cells.Add(headerCell);
        headerCell = new TableHeaderCell();
        headerCell.Text = "Descripton: ";
        head.Cells.Add(headerCell);
        headerCell = new TableHeaderCell();
        headerCell.Text = "Quantity: ";
        head.Cells.Add(headerCell);
        headerCell = new TableHeaderCell();
        headerCell.Text = "Total Price: ";
        head.Cells.Add(headerCell);

        tbl.Rows.AddAt(0, head);
    }
    private void footer(Table tbl)
    {
        TableRow footer = new TableRow();
        TableCell footerCell = new TableCell();
        footerCell.Text = "Order Total Cost: ";
        footer.Cells.AddAt(0, footerCell);

        footerCell = new TableCell();
        footerCell.Text = this.total.ToString();
        footer.Cells.AddAt(1, footerCell);

        tbl.Rows.Add(footer);
    }
}