import React from "react";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import { Link } from "react-router-dom";
import { DemoContainer, DemoItem } from "@mui/x-date-pickers/internals/demo";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { MobileDatePicker } from "@mui/x-date-pickers/MobileDatePicker";
import dayjs from "dayjs";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";

const SignUp = () => {
  return (
    <div className="container w-25 my-5 d-block shadow p-3 mb-5 bg-white rounded">
      <h2>SignUp</h2>
      <TextField
        id="standard-basic"
        className="mt-2"
        label="Name"
        type="text"
        variant="standard"
      />
      <TextField
        id="standard-basic"
        className="mt-2"
        label="Email"
        type="text"
        variant="standard"
      />
      <div className="d-flex justify-content-center mt-2">
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <DemoContainer components={["MobileDatePicker"]}>
            <MobileDatePicker label="Date of Birth" />
          </DemoContainer>
        </LocalizationProvider>
      </div>
      <FormControl
        variant="standard"
        className="mt-2"
        sx={{ minWidth: "200px" }}
      >
        <InputLabel id="demo-simple-select-standard-label">Role</InputLabel>
        <Select
          labelId="demo-simple-select-standard-label"
          id="demo-simple-select-standard"
          //   value={age}
          //   onChange={handleChange}
          label="Role"
        >
          <MenuItem value={"teacher"}>Teacher</MenuItem>
          <MenuItem value={"student"}>Student</MenuItem>
        </Select>
      </FormControl>
      <TextField
        id="standard-basic"
        className="mt-2"
        label="Password"
        type="password"
        variant="standard"
      />
      <TextField
        id="standard-basic"
        className="mt-2"
        label="Confirm Password"
        type="password"
        variant="standard"
      />
      <div className="d-flex justify-content-center my-3">
        <Button variant="outlined" className="d-block text-dark border-dark">
          SignUp
        </Button>
      </div>
      <div>
        <p>
          Already have an account?<Link to="/login">LogIn</Link>
        </p>
      </div>
    </div>
  );
};

export default SignUp;
