using AutoMapper;
using BasicDesk.App.Models.Common.BindingModels;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Data.Models.Solution;
using System.Linq;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Data.Models;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.App.Models.Management.BindingModels;

namespace BasicDesk.App.AutoMapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            this.CreateMap<User, UserConciseViewModel>();
            this.CreateMap<User, UserDetailsViewModel>();
            this.CreateMap<RequestCreationBindingModel, Request>()
                .ForMember(r => r.CategoryId, opt => opt.MapFrom(r => r.CategoryId))
                .ForMember(r => r.Attachments, opt => opt.Ignore());
            ;

            this.CreateMap<Request, RequestListingViewModel>()
                .ForMember(r => r.Requester, opt => opt.MapFrom(req => req.Requester.FullName))
                .ForMember(r => r.AssignedTo, opt => opt.MapFrom(req => req.AssignedTo.FullName))
                .ForMember(r => r.Status, opt => opt.MapFrom(req => req.Status.Name));

            this.CreateMap<Solution, SolutionListingViewModel>()
                .ForMember(r => r.Author, opt => opt.MapFrom(s => s.Author.FullName));
            this.CreateMap<SolutionCreationBindingModel, Solution>();

            this.CreateMap<Request, RequestDetailsViewModel>()
                .ForMember(r => r.CreatedOn, opt => opt.MapFrom(req => req.StartTime.ToString()))
                .ForMember(r => r.Status, opt => opt.MapFrom(req => req.Status.Name))
                .ForMember(r => r.Author, opt => opt.MapFrom(req => req.Requester.FullName))
                .ForMember(r => r.Category, opt => opt.MapFrom(req => req.Category.Name))
                .ForMember(r => r.Attachments, opt => opt.MapFrom(req => req.Attachments));

            this.CreateMap<CategoryCreationBindingModel, RequestCategory>();
            this.CreateMap<RequestCategory, CategoryViewModel>();

            this.CreateMap<Solution, SolutionDetailsViewModel>()
                  .ForMember(s => s.Attachment, opt => opt.MapFrom(sol => sol.Attachments.FirstOrDefault()))
                  .ForMember(s => s.Author, opt => opt.MapFrom(sol => sol.Author.FullName))
                  .ForMember(s => s.CreatedOn, opt => opt.MapFrom(sol => sol.CreationTime.ToString()));

        }
    }
}
