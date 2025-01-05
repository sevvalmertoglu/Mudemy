import { BrowserRouter, Route, Routes } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
import { CartProvider } from "./context/CartContext";
import Navbar from "./components/Navbar";
import Footer from "./components/Footer";
import Home from "./pages/Home";
import PaymentForm from "./pages/PaymentForm";
import Login from "./pages/Login";
import Profile from "./pages/Profile";
import Cart from "./pages/Cart";
import CourseDetail from "./pages/CourseDetail";
import Register from "./pages/Register";
import "./App.css";

export default function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <CartProvider>
          <div className="App">
            <Navbar />
            <div className="App-main">
              <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/cart" element={<Cart />} />
                <Route path="/profile" element={<Profile />} />
                <Route path="/login" element={<Login />} />
                <Route path="/payment" element={<PaymentForm />} />
                <Route path="/product/:id" element={<CourseDetail />} />
                <Route path="/register" element={<Register />} />
              </Routes>
            </div>
            <Footer className="App-footer" />
          </div>
        </CartProvider>
      </BrowserRouter>
    </AuthProvider>
  );
}