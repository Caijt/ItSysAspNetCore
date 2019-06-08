using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using ItSys.Entity;
using ItSys.Dto;
using System.Linq;

namespace ItSys.Service.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //创建SysUser往SysUserDTO转换的映射，ReverseMap是创建反向映射，不过我发现如果都是同名的属性的话，没加这个ReverseMap也是能反向映射的
            #region SysUser实体转换
            CreateMap<SysUser, SysUserDto>().ForMember(d => d.create_user_name, opts =>
                   {
                       opts.MapFrom(s => s.CreateUser.Name);
                   }).ReverseMap();
            CreateMap<SysUserSaveDto, SysUser>().ReverseMap();
            #endregion

            #region SysUserLoginLog
            CreateMap<SysUserLoginLogDto, SysUserLoginLog>().ReverseMap(); 
            #endregion

            #region SysMenu
            CreateMap<SysMenu, SysMenuDto>().ForMember(d => d.create_user_name, opts =>
                {
                    opts.MapFrom(s => s.CreateUser.Name);
                }).ReverseMap();
            CreateMap<SysMenuSaveDto, SysMenu>().ForMember(d => d.ParentId, opts =>
            {
                opts.MapFrom(s => s.parent_id);
            }).ReverseMap();
            #endregion

            #region SysCompany
            CreateMap<SysCompanySaveDto, SysCompany>()
                .ForMember(d => d.IsDisabled, options => { options.MapFrom(s => s.is_disabled); })
                .ReverseMap();
            CreateMap<SysCompany, SysCompanyDto>()
                .ForMember(d => d.create_user_name, opts =>
                {
                    opts.MapFrom(s => s.CreateUser.Name);
                }).ReverseMap();
            #endregion

            #region SysRole
            CreateMap<SysRoleSaveDto, SysRole>().ForMember(d => d.MenuIds, opts =>
            {
                opts.MapFrom(c => c.menu_ids);
            }).ReverseMap();
            #endregion

            #region SysUpdateRecord
            CreateMap<SysUpdateRecordSaveDto, SysUpdateRecord>().ReverseMap();
            #endregion

            #region Attach
            CreateMap<AttachSaveDto, Attach>().ReverseMap();
            #endregion

            #region HrDep
            CreateMap<HrDepDto, HrDep>().ReverseMap();
            #endregion

            #region HrEmployee
            CreateMap<HrEmployeeSaveDto, HrEmployee>().ReverseMap();
            CreateMap<HrEmployee, HrEmployeeDto>().ReverseMap();
            #endregion

            #region ItSupplier
            CreateMap<ItSupplierSaveDto, ItSupplier>().ReverseMap();
            CreateMap<ItSupplier, ItSupplierDto>().ReverseMap();
            #endregion

            #region ItAssetType
            CreateMap<ItAssetTypeDto, ItAssetType>().ReverseMap();
            #endregion

            #region ItAsset
            CreateMap<ItAssetSaveDto, ItAsset>().ReverseMap();
            CreateMap<ItAsset, ItAssetDto>()
                .ForMember(d => d.remain, o => o.MapFrom(s => s.amount - s.used - s.scrap_amount))
                .ForMember(d => d.avaiable_amount, o => o.MapFrom(s => s.amount - s.scrap_amount))
                .ReverseMap();
            #endregion

            #region ItAssetStockWarning
            CreateMap<ItAssetStockWarningSaveDto, ItAssetStockWarning>().ReverseMap();
            CreateMap<ItAssetStockWarning, ItAssetStockWarningDto>().ReverseMap();
            #endregion

            #region ItAccount
            CreateMap<ItAccountSaveDto, ItAccount>().ReverseMap();
            CreateMap<ItAccount, ItAccountDto>().ReverseMap();
            #endregion
            #region ItNetwork
            CreateMap<ItNetworkSaveDto, ItNetwork>().ReverseMap();
            CreateMap<ItNetwork, ItNetworkDto>().ReverseMap();
            #endregion
            #region ItContract
            CreateMap<ItContractSaveDto, ItContract>().ReverseMap();
            CreateMap<ItContract, ItContractDto>().ReverseMap();
            CreateMap<ItContractView, ItContractDto>().ReverseMap();
            #endregion
            #region ItContractPayRecord
            CreateMap<ItContractPayRecordSaveDto, ItContractPayRecord>().ReverseMap();
            CreateMap<ItContractPayRecord, ItContractPayRecordDto>().ReverseMap();
            #endregion
            #region ItAssetUseRecord
            CreateMap<ItAssetUseRecordSaveDto, ItAssetUseRecord>()
                .ForMember(d => d.input_status, o => o.MapFrom(s => s.action))
                .ReverseMap();
            CreateMap<ItAssetUseRecord, ItAssetUseRecordDto>().ReverseMap();
            CreateMap<ItAssetReturnRecordSaveDto, ItAssetUseRecord>()
                .ForMember(d => d.input_status, o => o.MapFrom(s => s.action))
                .ReverseMap();
            CreateMap<ItAssetUseRecord, ItAssetReturnRecordDto>().ReverseMap();
            #endregion
            #region ItAssetUseRecordItem
            CreateMap<ItAssetUseRecordItemSaveDto, ItAssetUseRecordItem>().ReverseMap();
            CreateMap<ItAssetReturnRecordItemSaveDto, ItAssetUseRecordItem>().ReverseMap();
            #endregion
            #region ItAssetRepairRecord
            CreateMap<ItAssetRepairRecordSaveDto, ItAssetRepairRecord>().ReverseMap();
            CreateMap<ItAssetRepairRecord, ItAssetRepairRecordDto>().ReverseMap();
            #endregion
            #region ItAssetScrapRecord
            CreateMap<ItAssetScrapRecordSaveDto, ItAssetScrapRecord>().ReverseMap();
            CreateMap<ItAssetScrapRecord, ItAssetScrapRecordDto>().ReverseMap();
            #endregion
        }
    }
}
