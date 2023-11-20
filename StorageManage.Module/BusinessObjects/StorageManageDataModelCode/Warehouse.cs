using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace StorageManage.Module.BusinessObjects.StorageManageDataModelCode
{
    [DefaultClassOptions, ImageName("BO_Warehouse")]
    public class Warehouse : XPObject
    {
        public Warehouse(Session session) : base(session){}

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        // Атрибут для обязательного поля при заполнении
        [RuleRequiredField(DefaultContexts.Save)]
        public string WarehouseName
        {
            get { return fWarehouseName; }
            set { SetPropertyValue(nameof(WarehouseName), ref fWarehouseName, value); }
        }
        string fWarehouseName;

        // Ссылка один к многим к пикетам
        [Association]
        public XPCollection<Picket> Pickets
        {
            get { return GetCollection<Picket>(nameof(Pickets)); }
        }

        //  !!!!Для теста, отслеживание изменений складов - Подключил аудит!!!!
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