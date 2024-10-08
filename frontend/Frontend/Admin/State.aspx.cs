﻿using Frontend.Admin;
using Frontend.Logic.Implementations;
using Frontend.Logic.Interfaces;
using Frontend.Models;
using Frontend.Models.Mappers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelReservationSystem_Part1.Admin
{
    public partial class State : System.Web.UI.Page
    {
        private static readonly ICountryDAO _countryDao = new CountryDao();
        private static readonly IStateDAO _stateDao = new StateDao();
        private static readonly ISecureDAO _secure=new SecureDao(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCountries();
                BindStates();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlCountry.SelectedIndex = 0;
            txtStateName.Text = string.Empty;   
        }

        protected void btnAddState_Click(object sender, EventArgs e)
        {
            string Message=string.Empty;
            if (validate(out Message))
            {
                dynamic state = new State_Model();
                state.StateName = txtStateName.Text.ToString();
                state.CountryId = Convert.ToInt64(ddlCountry.SelectedItem.Value);
                if (state != null)
                {
                    var enc_state = _secure.Encrypt(JsonConvert.SerializeObject(state), ConfigurationManager.AppSettings["key"],ConfigurationManager.AppSettings["iv"]);
                    var res=_stateDao.Create(enc_state);
                    if (res != null)
                    {
                        var dec_Res = _secure.Decrypt(res, ConfigurationManager.AppSettings["key"], ConfigurationManager.AppSettings["iv"]);
                        if (dec_Res != null)
                            Response.Write("<script>alert('Creation of state successfull)'</script>");
                    }
                }
            }
            else{
                Response.Write($"<script>alert('{Message}')</script>");
            }
            BindStates();
            BindCountries();
            clear();
        }
        private void BindStates()
        {
            List<State_Model> stateList = new List<State_Model>();
            var res = _stateDao.GetAll().Result;
            var dec_res = _secure.Decrypt(res, ConfigurationManager.AppSettings["key"], ConfigurationManager.AppSettings["iv"]);
            List<State_DTO> listDTO=JsonConvert.DeserializeObject<List<State_DTO>>(dec_res);
            stateList=listDTO.Select(x=>x.FromDTOToModel()).ToList();
            rptStateList.DataSource = stateList;
            rptStateList.DataBind();
        }
        private void BindCountries()
        {
            List<Country_Model> countryList = new List<Country_Model>();
            var res = _countryDao.GetAll().Result;
            var dec_res = _secure.Decrypt(res, ConfigurationManager.AppSettings["iv"], ConfigurationManager.AppSettings["key"]);
            countryList =JsonConvert.DeserializeObject<List<Country_Model>>(dec_res);
            ddlCountry.DataSource = countryList;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("Please Select the Country","0"));
            
        }
        private void clear()
        {
            txtStateName.Text = string.Empty;
            ddlCountry.SelectedItem.Value = "0";
        }

        private bool validate(out string message)
        {
            string Message = string.Empty;
            if (txtStateName.Text == null)
            {
                Message += "Please enter the state name ,";
            }
            if (ddlCountry.SelectedIndex == 0)
            {
                Message += "Please select the country ,";
            }
            if (Message != string.Empty)
            {
                message = Message.ToString();
               
                return false;
            }
            else
            {
                message = string.Empty;
                return true;
            }
            
        }
    }
}