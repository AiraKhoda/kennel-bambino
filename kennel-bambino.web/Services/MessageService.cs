using kennel_bambino.web.Data;
using kennel_bambino.web.Interfaces;
using kennel_bambino.web.Models;
using kennel_bambino.web.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MessageService> _logger;

        public MessageService(ApplicationDbContext context, ILogger<MessageService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Add new Contact to database
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public Contact AddContact(Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                _context.SaveChanges();

                return contact;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Contact> AddContactAsync(Contact contact)
        {
            try
            {
                await _context.Contacts.AddAsync(contact);
                await _context.SaveChangesAsync();

                return contact;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public int ContactsCount() => _context.Contacts.Count();


        public async Task<int> ContactsCountAsync() => await _context.Contacts.CountAsync();
        public ContactPagingViewModel GetContacts(int pageNumber = 1, int pageSize = 24)
        {
            IQueryable<Contact> contacts = _context.Contacts;

            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int petsCount = contacts.Count();

            int pageCount = (int)Math.Ceiling(decimal.Divide(petsCount, take));

            return new ContactPagingViewModel
            {
                Contacts = contacts.Skip(skip)
                .Take(take)
                .Where(c => c.IsChecked || c.IsChecked == false)
                .OrderByDescending(c => c.ContactId)
                .ToList(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };
        }

        public async Task<ContactPagingViewModel> GetContactsAsync(int pageNumber = 1, int pageSize = 24)
        {
            IQueryable<Contact> contacts = _context.Contacts;

            int take = pageSize;
            int skip = (pageNumber - 1) * take;
            int petsCount = await contacts.CountAsync();

            int pageCount = (int)Math.Ceiling(decimal.Divide(petsCount, take));

            return new ContactPagingViewModel
            {
                Contacts = await contacts.Skip(skip)
                .Take(take)
                .Where(c => c.IsChecked || c.IsChecked == false)
                .OrderByDescending(c => c.ContactId)
                .ToListAsync(),
                PageNumber = pageNumber,
                PageCount = pageCount
            };
        }


        /// <summary>
        /// Get All Contacts
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Contact> GetAllContacts() => _context.Contacts.ToList();


        public async Task<IEnumerable<Contact>> GetAllContactsAsync() => await _context.Contacts.ToListAsync();

        /// <summary>
        /// Get Contact by id from database
        /// </summary>
        /// <param name="contactId"></param>
        /// <returns></returns>
        public Contact GetContactById(int contactId) => _context.Contacts.SingleOrDefault(c => c.ContactId == contactId);


        public async Task<Contact> GetContactByIdAsync(int contactId) => await _context.Contacts.SingleOrDefaultAsync(c => c.ContactId == contactId);

        /// <summary>
        /// Remove the Contact from database
        /// </summary>
        /// <param name="contactId"></param>
        public void RemoveContact(int contactId)
        {
            var contact = GetContactById(contactId);

            _context.Contacts.Remove(contact);
            _context.SaveChanges();
        }

        public async Task RemoveContactAsync(int contactId)
        {
            var contact = await GetContactByIdAsync(contactId);

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Update the Contact's data from database
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public Contact UpdateContact(Contact contact)
        {
            try
            {
                _context.Contacts.Update(contact);
                _context.SaveChanges();

                return contact;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public async Task<Contact> UpdateContactAsync(Contact contact)
        {
            try
            {
                _context.Contacts.Update(contact);
                await _context.SaveChangesAsync();

                return contact;
            }
            catch (Exception ex)
            {

                _logger.LogError($"{ex.StackTrace}\n{ex.Message}");

                return null;
            }
        }

        public int NewContactsCount() => _context.Contacts.Where(c => c.IsChecked == false).Count();
        

        public async Task<int> NewContactsCountAsync() => await _context.Contacts.Where(c => c.IsChecked == false).CountAsync();

    }
}
