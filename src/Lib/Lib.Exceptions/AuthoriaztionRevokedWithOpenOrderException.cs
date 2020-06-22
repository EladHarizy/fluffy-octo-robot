using System;
using Lib.Entities;

namespace Lib.Exceptions {
    public class AuthoriaztionRevokedWithOpenOrderException : System.Exception {
        public Order Order { get; }

        public Host Host { get; }

        public AuthoriaztionRevokedWithOpenOrderException() {}

        public AuthoriaztionRevokedWithOpenOrderException(string message) : base(message) {}

        public AuthoriaztionRevokedWithOpenOrderException(Host host, Order order) : this(host, order, "Error: Billing authorization for host " + host.ID + " cannot be revoked. Order number " + order.ID + " is still opened.") {}

        public AuthoriaztionRevokedWithOpenOrderException(Host host, Order order, string message) : base(message) {
            Host = host;
            Order = order;
        }

        public AuthoriaztionRevokedWithOpenOrderException(string message, System.Exception inner) : base(message, inner) {}

    }
}