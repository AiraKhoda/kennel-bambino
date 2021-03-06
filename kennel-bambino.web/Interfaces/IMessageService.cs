﻿using kennel_bambino.web.Models;
using kennel_bambino.web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Interfaces
{
    public interface IMessageService
    {
        Contact AddContact(Contact contact);
        Task<Contact> AddContactAsync(Contact contact);
        IEnumerable<Contact> GetAllContacts();
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Contact GetContactById(int contactId);
        Task<Contact> GetContactByIdAsync(int contactId);
        Contact UpdateContact(Contact contact);
        Task<Contact> UpdateContactAsync(Contact contact);
        void RemoveContact(int contactId);
        Task RemoveContactAsync(int contactId);
        ContactPagingViewModel GetContacts(int pageNumber, int pageSize);
        Task<ContactPagingViewModel> GetContactsAsync(int pageNumber, int pageSize);
        int ContactsCount();
        Task<int> ContactsCountAsync();
        int NewContactsCount();
        Task<int> NewContactsCountAsync();

    }
}

