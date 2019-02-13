using AutoMapper;
using BasicDesk.App.Models.Common.BindingModels;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Data.Models.Solution;
using System.Linq;
using BasicDesk.Data.Models.Requests;
using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Data.Models;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.App.Models.Management.BindingModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasicDesk.Services.AutoMapping
{
    public static class AutoMapperConfig
    {
        private static bool initialized;

        public static void RegisterMappings()
        {
            if (initialized)
            {
                return;
            }

            initialized = true;

            Mapper.Initialize(configuration =>
            {
                configuration.CreateMap<User, UserConciseViewModel>();

                configuration.CreateMap<RequestApproval, RequestApprovalViewModel>()
                .ForMember(rav => rav.Status, opt => opt.MapFrom(ra => ra.Status.Name));


                configuration.CreateMap<User, UserDetailsViewModel>()
                .ForMember(u => u.Phone, opt => opt.MapFrom(u => u.PhoneNumber));

                configuration.CreateMap<RequestReply, RequestReplyViewModel>()
                .ForMember(u => u.Author, opt => opt.MapFrom(u => u.Author.UserName))
                .ForMember(r => r.CreationTime, opt => opt.MapFrom(rep => rep.CreationTime));

                configuration.CreateMap<RequestCreationBindingModel, Request>()
					.ForMember(r => r.CategoryId, opt => opt.MapFrom(r => r.CategoryId))
					.ForMember(r => r.Attachments, opt => opt.Ignore());

                configuration.CreateMap<RequestCategory, SelectListItem>()
                    .ForMember(s => s.Value, opt => opt.MapFrom(r => r.Id))
                    .ForMember(s => s.Text, opt => opt.MapFrom(r => r.Name));

                configuration.CreateMap<RequestStatus, SelectListItem>()
                  .ForMember(s => s.Value, opt => opt.MapFrom(s => s.Id))
                  .ForMember(s => s.Text, opt => opt.MapFrom(s => s.Name));

                configuration.CreateMap<Request, RequestListingViewModel>()
					.ForMember(r => r.Requester, opt => opt.MapFrom(req => req.Requester.FullName))
					.ForMember(r => r.AssignedTo, opt => opt.MapFrom(req => req.AssignedTo.FullName))
					.ForMember(r => r.Status, opt => opt.MapFrom(req => req.Status.Name));

                configuration.CreateMap<Solution, SolutionListingViewModel>()
					.ForMember(r => r.Author, opt => opt.MapFrom(s => s.Author.FullName));
                configuration.CreateMap<SolutionCreationBindingModel, Solution>();

                configuration.CreateMap<Request, RequestDetailsViewModel>()
					.ForMember(r => r.CreatedOn, opt => opt.MapFrom(req => req.StartTime.ToString()))
					.ForMember(r => r.Status, opt => opt.MapFrom(req => req.Status.Name))
					.ForMember(r => r.Author, opt => opt.MapFrom(req => req.Requester))
					.ForMember(r => r.Category, opt => opt.MapFrom(req => req.Category.Name))
					.ForMember(r => r.Attachments, opt => opt.MapFrom(req => req.Attachments))
                    .ForMember(r => r.Notes, opt => opt.MapFrom(req => req.Notes))
                    .ForMember(r => r.Technician, opt => opt.MapFrom(req => req.AssignedTo))
                    .ForMember( r=> r.Replies, opt => opt.MapFrom(req => req.Repiles));

                configuration.CreateMap<Request, RequestManagingModel>()
                .ForMember(r => r.CreatedOn, opt => opt.MapFrom(req => req.StartTime.ToString()))
                .ForMember(r => r.Author, opt => opt.MapFrom(req => req.Requester))
                .ForMember(r => r.Attachments, opt => opt.MapFrom(req => req.Attachments))
                .ForMember(r => r.Category, opt => opt.MapFrom(req => req.Category.Name))
                .ForMember(r => r.Status, opt => opt.MapFrom(req => req.Status.Name))
                .ForMember(r => r.Notes, opt => opt.MapFrom(req => req.Notes))
                .ForMember(r => r.Technician, opt => opt.MapFrom(req => req.AssignedTo))
                .ForMember(r => r.Replies, opt => opt.MapFrom(req => req.Repiles)); ;

                configuration.CreateMap<RequestNote, RequestNoteViewModel>()
                    .ForMember(rn => rn.Author, opt => opt.MapFrom(r => r.Author))
                    .ForMember(rn => rn.RequestId, opt => opt.MapFrom(r => r.RequestId));

                configuration.CreateMap<CategoryCreationBindingModel, RequestCategory>();
                configuration.CreateMap<RequestCategory, CategoryViewModel>();

                configuration.CreateMap<Solution, SolutionDetailsViewModel>()
                    .ForMember(s => s.Author, opt => opt.MapFrom(sol => sol.Author.FullName))
                    .ForMember(s => s.Attachments, opt => opt.MapFrom(sol => sol.Attachments))
					.ForMember(s => s.CreatedOn, opt => opt.MapFrom(sol => sol.CreationTime.ToString()));												
            });
			//Mapper.Configuration.AssertConfigurationIsValid();
        }
    }
}