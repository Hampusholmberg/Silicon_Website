﻿using Microsoft.AspNetCore.Mvc;
using WebApp.Models.Components;
using WebApp.Models.Sections;
using WebApp.Models.Views;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var viewModel = new HomeIndexViewModel
            {
                Title = "Home",

                Showcase = new ShowcaseViewModel
                {
                    Id = "Showcase",
                    ShowcaseImage = new ImageViewModel { ImageUrl = "/images/showcase/showcase-taskmaster.png", AltText = "Showcase image", },
                    Title = "Task Management Assistant You're Gonna Love",
                    Text = "We offer you a new generation of task management system. Plan, manage & track all your tasks in one flexible tool.",

                    BrandsText = "Largest companies use our tool to work efficiently",
                    BrandImages = new List<ImageViewModel>
                    {
                        new () { ImageUrl ="/images/showcase/logoipsum1.svg", AltText = "Logotype for a brand that uses our services." },
                        new () { ImageUrl ="/images/showcase/logoipsum2.svg", AltText = "Logotype for a brand that uses our services." },
                        new () { ImageUrl ="/images/showcase/logoipsum3.svg", AltText = "Logotype for a brand that uses our services." },
                        new () { ImageUrl ="/images/showcase/logoipsum4.svg", AltText = "Logotype for a brand that uses our services." },
                    },

                    Link = new () { ControllerName = "", ActionName = "", Text = "Get started for free"  } 

                },

                Features = new FeaturesViewModel
                {
                    Id = "Features",
                    Title = "What Do You Get with Our Tool?",
                    Text = "Make sure all your tasks are organized so you can set the priorities and focus on important.",

                    Features = new List<FeatureViewModel>
                    {
                        new () {FeatureImage = new () 
                        { ImageUrl = "/images/features/chat.svg", AltText = "" }, FeatureTitle = "Comments on Tasks", FeatureDescription = "Id mollis consectetur congue egestas egestas suspendisse blandit justo." },

                        new () {FeatureImage = new () 
                        { ImageUrl = "/images/features/presentation.svg", AltText = "" }, FeatureTitle = "Tasks Analytics", FeatureDescription = "Non imperdiet facilisis nulla tellus Morbi scelerisque eget adipiscing vulputate." },

                        new () {FeatureImage = new () 
                        { ImageUrl = "/images/features/add-group.svg", AltText = "" }, FeatureTitle = "Multiple Assignees", FeatureDescription = "A elementum, imperdiet enim, pretium etiam facilisi in aenean quam mauris." },

                        new () {FeatureImage = new () 
                        { ImageUrl = "/images/features/bell.svg", AltText = "" }, FeatureTitle = "Notifications", FeatureDescription = "Diam, suspendisse velit cras ac. Lobortis diam volutpat, eget pellentesque viverra." },

                        new () {FeatureImage = new () 
                        { ImageUrl = "/images/features/tasks.svg", AltText = "" }, FeatureTitle = "Sections & Subtasks", FeatureDescription = "Mi feugiat hac id in. Sit elit placerat lacus nibh lorem ridiculus lectus." },

                        new () {FeatureImage = new () 
                        { ImageUrl = "/images/features/shield.svg", AltText = "" }, FeatureTitle = "Data Security", FeatureDescription = "Aliquam malesuada neque eget elit nulla vestibulum nunc cras." },
                    }

                }
            };

            ViewData["Title"] = viewModel.Title;

            return View(viewModel);
        }
    }
}