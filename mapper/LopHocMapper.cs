using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentAPIw5.model;
using StudentAPIw5.dto;
namespace StudentAPIw5.mapper
{
    public static class LopHocMapper
    {
        //create -> entity
        public static LopHoc ToEntity(this LopHocDTO.LopHocCreateDTO createLopHocDTO)
        {
            return new LopHoc
            {

                TenLop = createLopHocDTO.TenLop,
                ChuyenNganh = createLopHocDTO.ChuyenNganh
            };
        }
        // entity -> response
        public static LopHocDTO.Response ToResponse(this LopHoc lopHoc)
        {
            return new LopHocDTO.Response
            {
                MaLop = lopHoc.MaLop,
                TenLop = lopHoc.TenLop,
                ChuyenNganh = lopHoc.ChuyenNganh
            };
        }
        // update -> entity
        public static void updateEntity(this LopHoc lopHoc, LopHocDTO.LopHocUpdateDTO updateLopHocDTO)
        {
            lopHoc.TenLop = updateLopHocDTO.TenLop;
            lopHoc.ChuyenNganh = updateLopHocDTO.ChuyenNganh;
        }
        // entity -> response
        public static List<LopHocDTO.Response> ToResponseList(this IEnumerable<LopHoc> lopHocs)
        {
            return lopHocs.Select(lp => lp.ToResponse()).ToList();

        }

    }
}