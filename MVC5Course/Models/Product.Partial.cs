namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        //[限制欄位值必須輸入兩個數字1]
        public string ProductName { get; set; }
        [Required]
        [Range(1.0, 99.9)]
        [DisplayFormat(DataFormatString = "{0:C1}")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        public Nullable<bool> Active { get; set; }
        [Required]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
