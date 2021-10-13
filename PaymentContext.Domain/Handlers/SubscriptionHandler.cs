using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este cpf já está em uso");
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já existe");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.Country, command.ZipCode, command.State, command.City);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var student = new Student(name, email, document);
            var payment = new BoletoPayment(command.BarCode,
                                            command.BoletoNumber,
                                            command.PaidDate,
                                            command.ExpireDate,
                                            command.Total,
                                            command.TotalPaid,
                                            address,
                                            new Document(command.Document, command.PayerDocumentType),
                                            command.Payer,
                                            new Email(command.PayerEmail));
            subscription.AddPayment(payment);
            AddNotifications(name, document, email, subscription, address, student, payment);
            if (!IsValid)
                return new CommandResult(false, "Não foi possive realizar sua assinatura");
            _studentRepository.CreateSubscription(student);
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Bem vindo! Sua assinatura foi criada!");

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }
            if (_studentRepository.DocumentExists(command.Document))
                AddNotification("Document", "Este cpf já está em uso");
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já existe");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.Country, command.ZipCode, command.State, command.City);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var student = new Student(name, email, document);
            var payment = new PaypalPayment(command.TransactionCode,
                                            command.PaidDate,
                                            command.ExpireDate,
                                            command.Total,
                                            command.TotalPaid,
                                            address,
                                            new Document(command.Document, command.PayerDocumentType),
                                            command.Payer,
                                            new Email(command.PayerEmail));
            subscription.AddPayment(payment);
            AddNotifications(name, document, email, subscription, address, student, payment);
            if (!IsValid)
                return new CommandResult(false, "Não foi possive realizar sua assinatura");

            _studentRepository.CreateSubscription(student);
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo", "Bem vindo! Sua assinatura foi criada!");

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
