using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.Drawing;

namespace StorageManage.Module.BusinessObjects.StorageManageDataModelCode
{
    [DefaultClassOptions, ImageName("BO_Picket")]
    public class Picket : XPObject
    {
        public Picket(Session session) : base(session){}

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        // Атрибут для обязательного поля при заполнении
        [RuleRequiredField(DefaultContexts.Save)]
        public string PicketName
        {
            get { return fPicketName; }
            set { SetPropertyValue(nameof(PicketName), ref fPicketName, value); }
        }
        string fPicketName;


        // Ссылка один к многим к складу
        [Association]
        public Warehouse Warehouse
        {
            get { return fWarehouse; }
            set { SetPropertyValue(nameof(Warehouse), ref fWarehouse, value); }
        }
        Warehouse fWarehouse;

        // Ссылка один к многим к площадке
        [Association]
        public Square Square
        {
            get { return fSquare; }
            set { SetPropertyValue(nameof(Square), ref fSquare, value); }
        }
        Square fSquare;

        //  !!!!Для теста, отслеживание изменений пикетов - Подключил аудит!!!!
        private XPCollection<AuditDataItemPersistent> auditTrail;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AuditDataItemPersistent> AuditTrail
        {
            get
            {
                if (auditTrail == null)
                {
                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return auditTrail;
            }
        }
    }

}