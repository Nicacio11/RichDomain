
using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Email email, Document document)
        {
            Name = name;
            Email = email;
            Document = document;
           // Address = address;
            _subscriptions = new List<Subscription>();
            AddNotifications(name, email, document);
        }

        //Open Close principle - classe está aberta para extensões mas fechada para modificações
        //caso queira mudar um nome precisa criar um méthodo

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Document Document { get; private set; }
        public Address Address { get; private set; }

        public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions.ToArray();
        public void AddSubscription(Subscription subscription)
        {
            //se já tiver uma assinatura ativa, cancela;

            //cancela todas as outras assnaturas, e coloa esta como principal

            // foreach (var sub in Subscriptions)
            // {
            //     sub.Inactivate();
            // }

            var hasSubscriptionActive = false;
            foreach (var sub in _subscriptions)
            {
                if (sub.Active)
                {
                    hasSubscriptionActive = true;
                }
            }
            if (hasSubscriptionActive)
                AddNotification("Student.Subscriptions", "Você já tem uma assinatura");
            if (subscription.Payments.Count == 0)
                AddNotification("Student.Subscriptions.Payments", "Está assinatura não possui pagamento");
            _subscriptions.Add(subscription);

        }

    }
}
