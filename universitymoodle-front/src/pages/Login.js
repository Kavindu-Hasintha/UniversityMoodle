import React from "react";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import { Link } from "react-router-dom";

const Login = () => {
  return (
    <div className="container w-25 my-5 d-block shadow p-3 mb-5 bg-white rounded">
      <h2>LogIn</h2>
      <TextField
        id="standard-basic"
        className="mt-2"
        label="Username"
        type="text"
        variant="standard"
      />
      <TextField
        id="standard-basic"
        className="mt-2"
        label="Password"
        type="password"
        variant="standard"
      />
      <div className="d-flex justify-content-center my-3">
        <Button variant="outlined" className="d-block text-dark border-dark">
          LogIn
        </Button>
      </div>
      <div>
        <p>
          Don't have an account?<Link to="/signup">SignUp</Link>
        </p>
      </div>
    </div>
  );
};

export default Login;
