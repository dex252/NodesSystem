using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NodesDLL
{
    [Table("units")]
    public class Unit
    {
        public int key { get; set; }
        [Key]
        public int? id { get; set; }
        [DisplayName("Тип подразделения")]
        public string type { get; set; }
        [DisplayName("Наименование подразделения"), Required, StringLength(120)]
        public string name { get; set; }
        [DisplayName("Должность руководителя")]
        public string position { get; set; }
        [DisplayName("ФИО руководителя"), Required, StringLength(120)]
        public string fullname { get; set; }
        [DisplayName("Непосредственная подчиненность")]
        public string dependence { get; set; }
        [DisplayName("Должность курирующего высшего руководителя")]
        public string curatorial_position { get; set; }
        [DisplayName("ФИО курирующего руководителя")]
        public string curatorial_fullname { get; set; }
        [DisplayName("Дата создания")]
        public DateTime date_create { get; set; }
        [DisplayName("Основание создания (приказ)")]
        public string base_create { get; set; }
        [DisplayName("Количество ставок по штатному расписанию")]
        public int count_bets { get; set; }
        [DisplayName("Количество занятых ставок")]
        public int count_closed_bets { get; set; }
        [DisplayName("Дата реорганизации / ликвидации / закрытия")]
        public DateTime date_close { get; set; }
        [DisplayName("Основание для реорганизации / ликвидации / закрытия (приказ)")]
        public string base_close { get; set; }
        [DisplayName("Наличие положения о структурном подразделении")]
        public string availability { get; set; }
        [DisplayName("Примечание")]
        public string remark { get; set; }
    }
}
