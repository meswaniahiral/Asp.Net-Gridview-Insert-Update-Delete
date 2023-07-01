using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace gridview_iud
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                gvTeacher.DataSource = BindGridView();
                gvTeacher.DataBind();
            }

        }

        private DataTable BindGridView()
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=db1;Integrated Security=True;Pooling=False");
            string myQuery = "select TeacherId,FirstName,LastName,Status,Nationality,Grade from Teacher";
            SqlDataAdapter dap = new SqlDataAdapter(myQuery, con);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            return ds.Tables[0];
        }

        public SortDirection direction
        {
            get
            {
                if (ViewState["directionState"] == null)
                {
                    ViewState["directionState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["directionState"];
            }
            set
            {
                ViewState["directionState"] = value;
            }
        }
        protected void gvTeacher_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (direction == SortDirection.Ascending)
            {
                direction = SortDirection.Descending;
                sortingDirection = "Desc";


            }
            else
            {
                direction = SortDirection.Ascending;
                sortingDirection = "Asc";

            }
            DataView sortedView = new DataView(BindGridView());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            Session["SortedView"] = sortedView;
            gvTeacher.DataSource = sortedView;
            gvTeacher.DataBind();
        }

        protected void gvTeacher_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTeacher.PageIndex = e.NewPageIndex;
            if (Session["SortedView"] != null)
            {
                gvTeacher.DataSource = Session["SortedView"];
                gvTeacher.DataBind();
            }
            else
            {
                gvTeacher.DataSource = BindGridView();
                gvTeacher.DataBind();
            }
        }
       
    }
}