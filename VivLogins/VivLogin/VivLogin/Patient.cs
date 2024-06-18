using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace VivLogin
{
    public class Patient
    {
        private readonly List<PatientData> patients;

        public Patient()
        {
            patients = new List<PatientData>();
        }

        public List<PatientData> MakeList(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var row = array[i].Split(',', (char)StringSplitOptions.RemoveEmptyEntries);

                patients.Add
                (
                    new PatientData
                    {
                        Name = row[0],
                        Age = Convert.ToInt32(row[1]),
                        PersonNumber = row[2],
                        PhoneNumber = row[3],
                        Address = row[4],
                        Reserved = row[5] != null ? Convert.ToDateTime(row[5]) : default
                    }
                );
            }

            return patients;
        }

        public void InsertPatientInfo(string name, string age, string personNumber, string phoneNumber, string address)
        {
            //여기까지가 텍스트박스에 입력한 텍스트 불러오기--이걸 텍스트파일안에 탭기준으로 나눠서 한줄로 넣기
            string patient = "patient.txt";

            string record = $"{name}, {age}, {personNumber}, {phoneNumber}, {address}, {string.Empty}{Environment.NewLine}";

            File.AppendAllText(patient, record);
        }

        /// <summary>
        /// 환자 검색
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IEnumerable<PatientData> GetPerson(string name)
        {
            IEnumerable<PatientData> searched = patients.Where(x => x.Name.Equals(name));

            return searched.Count() > 0 ? searched : null;
        }


    }


    /// <summary>
    /// Text Database Model Class
    /// </summary>
    public class PatientData
    {
        [DisplayName("이름")]
        public string Name { get; set; }

        [DisplayName("나이")]
        public int Age { get; set; }

        [DisplayName("주민등록번호")]
        public string PersonNumber { get; set; }

        [DisplayName("전화번호")]
        public string PhoneNumber { get; set; }

        [DisplayName("주소")]
        public string Address { get; set; }

        [DisplayName("예약날짜")]
        public DateTime? Reserved { get; set; }
    }
}
