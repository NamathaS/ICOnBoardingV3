using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ICOnBoardingV3Knockout.Models
{
    public class SalesViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        [Required]

        public int CustomerId { get; set; }
        [Required]

        public int StoreId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateSold { get; set; }
        public ProductsViewModel SelectedProduct { get; set; }
        public CustomerViewModel SelectedCustomer { get; set; }
        public StoreViewModel SelectedStore { get; set; }
        public virtual IList<ProductsViewModel> Products { get; set; }
        public virtual IList<CustomerViewModel> Customers { get; set; }

        public virtual IList<StoreViewModel> Stores { get; set; }

    }
}