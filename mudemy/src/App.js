import { BrowserRouter, Route, Routes } from "react-router-dom";
import {AuthProvider} from "./context/AuthContext";
import {CartProvider} from "./context/CartContext";
import Navbar from "./components/Navbar";
import Footer from "./components/Footer";
import Home from "./pages/Home";
import PaymentForm from "./pages/PaymentForm";
import Login from "./pages/Login";
import Profile from "./pages/Profile";
import Cart from "./pages/Cart";
import ProductDetail from "./pages/ProductDetail";
import Register from './pages/Register';


export default function App(){
  return(
    <AuthProvider>
      <BrowserRouter>
        <CartProvider>
          <Navbar/>
          <Routes>
            <Route path="/" element={<Home/>}></Route>
            <Route path="/cart" element={<Cart/>}></Route>
            <Route path="/profile" element={<Profile/>}></Route>
            <Route path="/login" element={<Login/>}></Route>
            <Route path="/payment" element={<PaymentForm/>}></Route>
            <Route path="/product/:id" element={<ProductDetail/>}></Route>
            <Route path="/register" element={<Register/>}></Route>
          </Routes>
          <Footer></Footer>
        </CartProvider>
      </BrowserRouter>
    </AuthProvider>
  );
}