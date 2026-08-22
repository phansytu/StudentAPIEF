using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw6.API.DTOs.Request;
using StudentAPIw6.API.DTOs.Response;
using StudentAPIw6.Model;
namespace StudentAPIw6.AutoMapper
{
    public static class LopHocMapper
    {
        //create -> entity
        public static LopHoc ToEntity(this LopHocRequestDTO.LopHocCreateDTO createLopHocDTO)
        {
            return new LopHoc
            {
                TenLop = createLopHocDTO.TenLop,
                ChuyenNganh = createLopHocDTO.ChuyenNganh,
                BoMonId = createLopHocDTO.boMonId
            };
        }
        // entity -> response
        public static LopHocResponseDTO ToResponse(this LopHoc lopHoc)
        {
            return new LopHocResponseDTO
            {
                Id = lopHoc.Id,
                MaLop = lopHoc.MaLop,
                TenLop = lopHoc.TenLop,
                ChuyenNganh = lopHoc.ChuyenNganh,
                BoMonId = lopHoc.BoMonId
            };
        }
        // update -> entity
        public static void updateEntity(this LopHoc lopHoc, LopHocRequestDTO.LopHocUpdateDTO updateLopHocDTO)
        {
            lopHoc.TenLop = updateLopHocDTO.TenLop;
            lopHoc.ChuyenNganh = updateLopHocDTO.ChuyenNganh;
        }
        // entity -> response
        public static List<LopHocResponseDTO> ToResponseList(this IEnumerable<LopHoc> lopHocs)
        {
            return lopHocs.Select(lp => lp.ToResponse()).ToList();

        }

    }
}