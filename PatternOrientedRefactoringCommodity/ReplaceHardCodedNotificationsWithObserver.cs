using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternOrientedRefactoringCommodity
{
    //Subject:観察者
    public class Subject
    {
        List<Observer> observer_list_ = new List<Observer>();

        public void AddObserver(Observer observer)
        {
            observer_list_.Add(observer);
        }

        public void RemoveObserver(Observer observer)
        {
            observer_list_.Remove(observer);
        }

        // 全ての観測者のに対して通知をおこなう
        public void NotifyObserver()
        {
            foreach (Observer observer in observer_list_)
            {
                observer.Update();
            }
        }

        // 変更を通知
        public void SetChanged()
        {
            NotifyObserver();
        }
    }

    //会社を定義します。
    public class ConcreteCompany : Subject
    {
        string CompanyName_;
        string PresidentName_;

        public string CompanyName
        {
            set { this.CompanyName_ = value; }
            get { return CompanyName_; }

        }

        public string PresidentName
        {
            set { this.PresidentName_ = value; }
            get { return PresidentName_; }
        }

        public ConcreteCompany(string strCompanyName)
        {
            this.CompanyName_ = strCompanyName;
        }
    }

    //Observerのインターフェース
    public interface Observer
    {
        void Update();
    }

    // 観測対象者
    public class ConcreteObserverWorker : Observer
    {
        string EmployeeName_;
        ConcreteCompany Company_;

        // 観測対象者を登録
        public ConcreteObserverWorker(ConcreteCompany company, string employee_name)
        {
            this.EmployeeName_ = employee_name;
            this.Company_ = company;
        }

        public string EmployeeName
        {
            set { this.EmployeeName_ = value; }
            get { return EmployeeName_; }
        }

        public void Update()
        {
            string comment = string.Format("私の名前は{0}です。{1}に勤務してます。社長の名前は{2}さんです。", this.EmployeeName_, Company_.CompanyName, Company_.PresidentName);
            System.Diagnostics.Debug.WriteLine(comment);
        }
    }
}
