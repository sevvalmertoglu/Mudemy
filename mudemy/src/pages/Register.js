import React from 'react';
import { useAuth } from '../context/AuthContext';
import { useForm } from 'react-hook-form';
import { useNavigate } from 'react-router-dom';
import alertify from 'alertifyjs';

export default function Register() {
  const { registerUser } = useAuth();
  const { register, handleSubmit, formState: { errors } } = useForm();
  const navigate = useNavigate();

  const onSubmit = (data) => {
    registerUser(data.username, data.email, data.password);
    alertify.success("Kayıt başarılı!");
    navigate("/login"); 
  };

  return (
    <div className='container d-flex justify-content-center align-items-center' style={{ height: "100vh" }}>
      <div className='card shadow p-4' style={{ width: "500px", borderRadius: "20px", height: "500px" }}>
        <h2 className='text-center' style={{ color: "#EE4E34", fontSize: "45px", fontWeight: "bold" }}>Register</h2>
        <form onSubmit={handleSubmit(onSubmit)}>
          <div className='mb-3'>
            <label className='form-label' style={{ color: "#EE4E34", fontSize: "20px" }}>Username</label>
            <input
              {...register("username", { required: "Username is required" })}
              className='form-control'
              placeholder='Enter your username...'
              style={{
                border: '2px solid #EE4E34',
                borderRadius: '25px',
                padding: '10px',
                fontSize: '16px',
                width: '100%'
              }}
            ></input>
            {errors.username && <small className='text-danger'>{errors.username.message}</small>}
          </div>
          <div className='mb-3'>
            <label className='form-label' style={{ color: "#EE4E34", fontSize: "20px" }}>Email</label>
            <input
              {...register("email", {
                required: "Email is required",
                pattern: { value: /^\S+@\S+$/i, message: "Invalid email address" }
              })}
              className='form-control'
              placeholder='Enter your email...'
              style={{
                border: '2px solid #EE4E34',
                borderRadius: '25px',
                padding: '10px',
                fontSize: '16px',
                width: '100%'
              }}
            ></input>
            {errors.email && <small className='text-danger'>{errors.email.message}</small>}
          </div>
          <div className='mb-3'>
            <label className='form-label' style={{ color: "#EE4E34", fontSize: "20px" }}>Password</label>
            <input
              {...register("password", {
                required: "Password is required",
                minLength: { value: 6, message: "Password must be at least 6 characters" }
              })}
              type='password'
              className='form-control'
              placeholder='Enter your password...'
              style={{
                border: '2px solid #EE4E34',
                borderRadius: '25px',
                padding: '10px',
                fontSize: '16px',
                width: '100%'
              }}
            ></input>
            {errors.password && <small className='text-danger'>{errors.password.message}</small>}
          </div>
          <button
            type='submit'
            className='btn w-100'
            style={{
              borderRadius: "30px",
              height: "45px",
              backgroundColor: "#EE4E34",
              color: "white",
              fontSize: "22px"
            }}
          >
            Register
          </button>
        </form>
      </div>
    </div>
  );
}
