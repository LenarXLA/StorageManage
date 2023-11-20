using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace StorageManage.Module.BusinessObjects.StorageManageDataModelCode
{
    // Обработка атрибутом условий когда вес нулевой - выделяем поле красным цветом.
    [DefaultClassOptions, ImageName("BO_Cargo")]
    [Appearance("RedCargoWeightObject", AppearanceItemType = "ViewItem", TargetItems = "*",
    Criteria = "CargoWeight==0", Context = "ListView", BackColor = "Red",
        FontColor = "Maroon", Priority = 2)]
    public class Cargo : XPObject
    {
        public Cargo(Session session) : base(session){}

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        // Тип uint - вес только положительное число
        public uint CargoWeight
        {
            get { return fCargoWeight; }
            set { SetPropertyValue(nameof(CargoWeight), ref fCargoWeight, value); }
        }
        uint fCargoWeight;

        // Атрибут для обязательного поля при заполнении
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime CargoDate
        {
            get { return fCargoDate; }
            set { SetPropertyValue(nameof(CargoDate), ref fCargoDate, value); }
        }
        DateTime fCargoDate;

        // ...
        // Ссылка один на один к площадке
        // ...
        Square square = null;
        public Square Square
        {
            get { return square; }
            set
            {
                if (square == value)
                    return;

                // Сохранение предыдущей ссылки на площадку
                Square prevSquare = square;
                square = value;

                if (IsLoading) return;

                // Удаление ссылки на плащдку, если грузу уже на этой площадке
                if (prevSquare != null && prevSquare.Item == this)
                    prevSquare.Item = null;

                // Указать новую площадку для груза
                if (square != null)
                    square.Item = this;
                OnChanged(nameof(Square));
            }
        }
    }

}