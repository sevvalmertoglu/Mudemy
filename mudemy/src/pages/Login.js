import React from 'react'
import { useAuth } from '../context/AuthContext'
import { useCart } from '../context/CartContext';
import { useForm } from 'react-hook-form';
import { useNavigate, Link } from 'react-router-dom';
import alertify from 'alertifyjs';

export default function Login() {
  const {login} = useAuth();
  const {addPendingItem} = useCart();

  const {register, handleSubmit, formState: {errors, isSubmitted}} = useForm();

  const navigate = useNavigate();

  const onSubmit = (data) => {
    if(data.username === "admin" && data.password === "1234"){
      login(data.username, data.password);
      alertify.success("Giriş başarılı!");

      addPendingItem();
      navigate("/cart");
    }else{
      alertify.error("Kullanıcı adı veya parola hatalı!");
    }
  }
  
  return(
    <div className='container d-flex justify-content-center align-items-center' style={{height: "100vh"}}>
      <div className='card shadow p-4' style={{width: "500px", borderRadius: "20px", height: "400px"}}>
            <h2 className='text-center' style={{ color:"#EE4E34", fontSize: "45px", fontWeight: "bold" }}>Login</h2>
            <form onSubmit={handleSubmit(onSubmit)}>
              <div className='mb-3'>
                <label className='form-label' style={{ color:"#EE4E34", fontSize: "20px"}}>Username</label>
                <input
                {...register("username", {required: true})}
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
                {errors.username && <small className='text-danger'>Username is required</small>}
              </div>
              <div className='mb-3'>
                <label className='form-label'style={{ color:"#EE4E34", fontSize: "20px"}}>Password</label>
                <input
                {...register("password", {required: true})}
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
                {errors.password && <small className='text-danger'>Password is required</small>}
              </div>
              <button type='submit' className='btn w-100' style={{ borderRadius: "30px", height:"45px", backgroundColor: "#EE4E34", color: "white", fontSize: "22px"  }}>Login</button>
            </form>
            <div className='text-center mt-3'>
          <span>Don't have an account? </span>
          <Link to="/register" style={{ color: "#EE4E34", fontWeight: "bold" }}>Sign Up</Link>
        </div>
      </div>
    </div>
  );
}

