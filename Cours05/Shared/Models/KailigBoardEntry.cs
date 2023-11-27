using System.ComponentModel.DataAnnotations.Schema;

namespace SharedLibrary.Models
{
    [Table("iidba.kailig_board")]
    public class KailigBoardEntry
    {
         
        [Column("identifiant_ligne")]
        public string LineIdentifier { get; set; }

        [Column("col_init")]
        public string InitialColumn { get; set; }

        [Column("col_nompnom")]
        public string FullName { get; set; }

        [Column("lig_dat")]
        public DateTime DateField { get; set; }

        [Column("lig_dat_b")]
        public string DateFieldB { get; set; }

        [Column("lig_sem")]
        public string WeekField { get; set; }

        [Column("lig_no")]
        public int LineNumber { get; set; }

        [Column("pro_cod")]
        public string ProjectCode { get; set; }

        [Column("pro_des")]
        public string ProjectDescription { get; set; }

        [Column("lig_act")]
        public int ActivityNumber { get; set; }

        [Column("lig_act_abr")]
        public string ActivityAbbreviation { get; set; }

        [Column("lig_act_lib")]
        public string ActivityName { get; set; }

        [Column("lig_fac")]
        public int FactorNumber { get; set; }

        [Column("lig_fac_abr")]
        public string FactorAbbreviation { get; set; }

        [Column("lig_fac_lib")]
        public string FactorName { get; set; }

        [Column("lig_des")]
        public string Description { get; set; }

        [Column("lig_het")]
        public float HourExternal { get; set; }

        [Column("lig_hft")]
        public float? HourInternal { get; set; }

        [Column("remb")]
        public float? Reimbursement { get; set; }

        [Column("fact")]
        public float? InvoiceAmount { get; set; }
    }
}
