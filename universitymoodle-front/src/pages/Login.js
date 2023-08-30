import React from "react";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";

const Login = () => {
  return (
    <div className="container w-25 my-5 d-block shadow p-3 mb-5 bg-white rounded">
      <h2>LogIn</h2>
      <TextField
        id="standard-basic"
        label="Username"
        type="text"
        variant="standard"
      />
      <TextField
        id="standard-basic"
        label="Password"
        type="password"
        variant="standard"
      />
      <div className="d-flex justify-content-center">
        <Button variant="outlined" className="d-block">
          LogIn
        </Button>
      </div>
    </div>
  );
};

export default Login;
