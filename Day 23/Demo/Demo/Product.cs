using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    /// <summary>
    /// Entity Class Product
    /// </summary>
    public class Product
    {
        #region Fields
            int prodID;
            string prodName;
            int price;
            string description;
        #endregion


        #region Properties
        public int ProdId 
        {
            get { return prodID; }
            set
            {
                if(value<=0 || value >= 999)
                {
                    throw new MyCustomException("Product ID is not Valid");
                }
            }
        }

        public string ProdName { get; set; }
        public int ProdPrice { get; set; }
        public string CatName { get; set; }
        public string ProdDescription { get; set; }


        #endregion


    }
}
