﻿using System;
using System.Collections.Generic;
using System.Linq;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Catalog.Models
{
    /// <summary>
    /// Represents a category
    /// </summary>
    public class Category : IAggregateRoot
    {
        /// <summary>
        /// Get or set the category id
        /// </summary>
        public Guid Id { get; set; }

        #region Constructor
        protected Category()
        {
            _Children = new HashSet<Category>();
            _Products = new HashSet<ProductCategory>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the category unique code
        /// </summary>
        public string Code { get; protected set; }

        /// <summary>
        /// Get or set the category name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Get or set the category url (unique slug)
        /// </summary>
        public string Url { get; protected set; }

        /// <summary>
        /// Get or set a description for the category
        /// </summary>
        public string Description { get; protected set; }

        /// <summary>
        /// Get or set whether the category is visible
        /// </summary>
        public bool IsVisible { get; protected set; }

        /// <summary>
        /// Get or set the date and time from which the category is visible
        /// </summary>
        public DateTime? VisibleFrom { get; protected set; }

        /// <summary>
        /// Get or set the date and time till which the category is visible
        /// </summary>
        public DateTime? VisibleTo { get; protected set; }

        /// <summary>
        /// Get or set whether the category is deleted
        /// </summary>
        public bool Deleted { get; protected set; }

        /// <summary>
        /// Get or set the parent category
        /// </summary>
        public virtual Category Parent { get; protected set; }

        /// <summary>
        /// Get or set the list of children categories
        /// </summary>
        protected virtual ICollection<Category> _Children { get; set; }

        /// <summary>
        /// Get the list of childrend categories
        /// </summary>
        public IEnumerable<Category> Children => _Children;

        /// <summary>
        /// Get or set the list of products associated to the category
        /// </summary>
        protected virtual ICollection<ProductCategory> _Products { get; set; }

        /// <summary>
        /// Get the list of products associated to the category
        /// </summary>
        public IEnumerable<Product> Products => _Products.Select(p => p.Product);

        #endregion

        #region Behaviors

        /// <summary>
        /// Set the category as visible, starting from the current date and time
        /// </summary>
        public virtual void SetAsVisible()
        {
            SetAsVisible(DateTime.Now);
        }

        /// <summary>
        /// Set the category as visible
        /// </summary>
        /// <param name="from">The date and time from which the category is visible</param>
        public virtual void SetAsVisible(DateTime from)
        {
            IsVisible = true;
            VisibleFrom = from;
        }

        /// <summary>
        /// Set the category as visible
        /// </summary>
        /// <param name="from">The date and time from which the category is visible</param>
        /// <param name="to">The date and time till which the category is visible</param>
        public virtual void SetAsVisible(DateTime from, DateTime to)
        {
            if (from >= to)
            {
                throw new ArgumentException("the from date should be previous to the end date");
            }

            SetAsVisible(from);
            VisibleTo = to;
        }

        /// <summary>
        /// Add a children to the category
        /// </summary>
        /// <param name="child">The category children</param>
        public virtual void AddChildren(Category child)
        {
            if(child == null)
            {
                throw new ArgumentNullException("children");
            }

            if (_Children.Contains(child))
            {
                throw new ArgumentException("The category contains the children yet");
            }

            _Children.Add(child);
        }

        /// <summary>
        /// Change the category name
        /// </summary>
        /// <param name="name">The category name</param>
        public void ChangeName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
        }

        /// <summary>
        /// Change the category code
        /// </summary>
        /// <param name="code">The category code</param>
        public void ChangeCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }

            Code = code;
        }

        /// <summary>
        /// Change the category description
        /// </summary>
        /// <param name="description">The category description</param>
        public void ChangeDescription(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("description");
            }

            Description = description;
        }

        /// <summary>
        /// Change the category url
        /// </summary>
        /// <param name="url">The category url</param>
        public void ChangeUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            Url = url;
        }

        /// <summary>
        /// Set the parent category
        /// </summary>
        /// <param name="parent">The parent category</param>
        public void SetParentCategory(Category parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }

            Parent = parent;
        }

        /// <summary>
        /// Delete the category
        /// </summary>
        public void Delete()
        {
            if (Deleted)
            {
                throw new InvalidOperationException("The category is already deleted");
            }

            Deleted = true;
        }

        /// <summary>
        /// Restore the deleted category
        /// </summary>
        public void Restore()
        {
            if (!Deleted)
            {
                throw new InvalidOperationException("The category is not deleted");
            }

            Deleted = false;
        }

        #endregion

        #region Factory Methods
        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="code">The category unique code</param>
        /// <param name="name">The category name</param>
        /// <param name="url">The category url</param>
        /// <returns>The category created</returns>
        public static Category Create(string code, string name, string url)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentNullException("code");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Code = code,
                Name = name,
                Url = url
            };

            return category;
        }

        #endregion
    }
}
