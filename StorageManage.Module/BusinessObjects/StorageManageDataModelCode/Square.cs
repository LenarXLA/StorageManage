using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace StorageManage.Module.BusinessObjects.StorageManageDataModelCode
{

    // Аттрибут SquareDateObject - указывает курсиром площадки, которые созданы более 3 лет назад
    [DefaultClassOptions, ImageName("BO_Square")]
    [Appearance("SquareDateObject", AppearanceItemType = "ViewItem", TargetItems = "*",
    Criteria = "SquareDate<AddYears(LocalDateTimeToday(), -3)", Context = "ListView",
        FontStyle = FontStyle.Italic, Priority = 2)]
    public class Square : XPObject
    {

        public Square(Session session) : base(session){}

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        // Установка названия площадки - по выбранным пикетам, без возможности ручной установки
        [NotMapped]
        public string SquareName
        {
            get 
            {
                if (Pickets.Count != 0)
                {
                    return GenerateSquareName(Pickets.FirstOrDefault(), Pickets.LastOrDefault());
                }
                return "";
            }
        }
        string fSquareName;

        // Атрибут для обязательного поля при заполнении
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime SquareDate
        {
            get { return fSquareDate; }
            set { SetPropertyValue(nameof(SquareDate), ref fSquareDate, value); }
        }
        DateTime fSquareDate;


        // Ссылка один к многим к пикетам
        [Association]
        public XPCollection<Picket> Pickets
        {
            get { return GetCollection<Picket>(nameof(Pickets)); }
        }

        // ...
        // Ссылка один на один к грузу
        // ...
        Cargo item = null;
        public Cargo Item
        {
            get { return item; }
            set
            {
                if (item == value)
                    return;

                // Сохранение предыдущей ссылки на груз
                Cargo prevItem = item;
                item = value;

                if (IsLoading) return;

                // Удаление ссылки на груз, если площадке этот груз уже принадлежит
                if (prevItem != null && prevItem.Square == this)
                    prevItem.Square = null;

                // Указать новый груз для площадки
                if (item != null)
                    item.Square = this;

                OnChanged(nameof(Item));
            }
        }

        // Генерируем наименование площадки по правилу "первый пикет - последний пикет"
        private string GenerateSquareName(Picket p1, Picket p2)
        {
            var result = string.Empty;
            try
            {
                if (p1.PicketName == p2.PicketName)
                {
                    result = p1.PicketName;
                } 
                else
                {
                    result = string.Join("-", p1.PicketName, p2.PicketName);
                }
                
            }
            catch (Exception ex)
            {
                // TO-DO Перенаправить в логгер в будущем
                Console.WriteLine($"Ошибка при доступе к Picket. Ошибка: { ex.Message}"); ;
            }
            return result;
        }
    }

}