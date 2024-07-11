using CEO_simulator.MainLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEO_simulator.EntityInfomation
{
    internal class Company
    {
        //create attribute
        public double Turnover { get; set; }
        public int Level { get; set; }
        public List<Staff> StaffList { get; set; }
        //constructor
        public Company() 
        {
            StaffList = new List<Staff>();
            Level = 1;
        }
        /// <summary>
        /// method to add new staff
        /// </summary>
        /// <param name="num"></param>
        public void addStaff(int num = 1) 
        {
            //create a list of new staff
            var newStaffList = new List<Staff>();
            for (var i = 0; i < num; i++) {
                Staff newStaff = new Staff();
                newStaffList.Add(newStaff);
                //print new staff infor
                Console.WriteLine($"You have hired {newStaffList[newStaffList.Count - 1].StaffName}; ability: {newStaffList[newStaffList.Count - 1].StaffValueDefault}");
            }
            //add newStaffs to StaffList
            foreach (var newStaff in newStaffList)
            {
                StaffList.Add(newStaff);
                RecurList(StaffList.Count - 1);
            }
        }

        /// <summary>
        /// sort StaffList
        /// </summary>
        /// <param name="i"></param>
        /// //take the last staff 
        public void RecurList(int i) {
            //if the list is not empty
            if (i != 0)
            {
                //take the last staff and compare it to the one before it
                if (StaffList[i].StaffValueDefault > StaffList[i - 1].StaffValueDefault)
                {
                    //if the target Staff bigger than the on brfore, then swap them
                    Staff temp = StaffList[i-1];
                    StaffList[i - 1] = StaffList[i];
                    StaffList[i] = temp;
                    RecurList(i - 1);
                }
                //else do nothing

            }

        }


        /// <summary>
        /// method to remove Staff from StaffList
        /// </summary>
        /// <param name="targetStaff"></param>
        public void removeStaff(Staff targetStaff)
        {
            StaffList.Remove(targetStaff);
        }
        /// <summary>
        /// method to Calculate the Trunover of company
        /// </summary>
        /// <returns></returns>
        public double CalculateTrunover()
        {
            //Calculate sum value of staffs in company
            Turnover = StaffList.Sum(s => s.StaffValueDefault);
            return Turnover;
        }


    }
}
