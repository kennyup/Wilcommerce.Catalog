﻿using System;
using System.Collections.Generic;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represents a brand
    /// </summary>
    public class Brand : IAggregateRoot
    {
        /// <summary>
        /// Get or set the brand id
        /// </summary>
        public Guid Id { get; set; }

        #region Constructor
        protected Brand()
        {
            _Products = new HashSet<Product>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the brand name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Get or set a description for the brand
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Get or set the brand url (unique slug)
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Get or set whether the brand is deleted
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Get or set the brand logo
        /// </summary>
        public virtual Image Logo { get; protected set; }

        /// <summary>
        /// Get or set the seo data for the brand
        /// </summary>
        public virtual SeoData Seo { get; protected set; }

        /// <summary>
        /// Get or set the list of products associated to the brand
        /// </summary>
        protected virtual ICollection<Product> _Products { get; set; }

        /// <summary>
        /// Get the list of products associated to the brand
        /// </summary>
        public IEnumerable<Product> Products => _Products;

        #endregion

        #region Behaviors
        /// <summary>
        /// Change the brand name
        /// </summary>
        /// <param name="name">The new brand name</param>
        public virtual void ChangeName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
        }

        /// <summary>
        /// Change the brand description
        /// </summary>
        /// <param name="description">The brand description</param>
        public virtual void ChangeDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("description");
            }

            Description = description;
        }

        /// <summary>
        /// Change the brand url
        /// </summary>
        /// <param name="url">The new brand url</param>
        public virtual void ChangeUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            Url = url;
        }

        /// <summary>
        /// Set the brand logo
        /// </summary>
        /// <param name="logo">The logo image</param>
        public virtual void SetLogo(Image logo)
        {
            if (logo == null)
            {
                throw new ArgumentNullException("logo");
            }

            Logo = logo;
        }

        /// <summary>
        /// Set the brand seo data
        /// </summary>
        /// <param name="seo">The seo data</param>
        public virtual void SetSeoData(SeoData seo)
        {
            if (seo == null)
            {
                throw new ArgumentNullException("seo");
            }

            Seo = seo;
        }

        /// <summary>
        /// Set the brand as deleted
        /// </summary>
        public virtual void Delete()
        {
            if (Deleted)
            {
                throw new InvalidOperationException("The brand is already deleted");
            }

            Deleted = true;
        }

        /// <summary>
        /// Restore the deleted brand
        /// </summary>
        public virtual void Restore()
        {
            if (!Deleted)
            {
                throw new InvalidOperationException("The brand is not deleted");
            }

            Deleted = false;
        }
        #endregion

        #region Factory Methods
        /// <summary>
        /// Create a new Brand
        /// </summary>
        /// <param name="name">The brand name</param>
        /// <param name="url">The brand url</param>
        /// <returns>The brand created</returns>
        public static Brand Create(string name, string url)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                Name = name,
                Url = url
            };

            return brand;
        }

        #endregion
    }
}
