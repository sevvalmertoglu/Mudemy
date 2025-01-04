import { Link } from "react-router-dom";
import { useAuth } from "../context/AuthContext";
import "../styles/Navbar.css";
import "../images/cartt.png";

export default function Navbar(){

    const {user, logout} = useAuth();

    return(
        <nav className="navbar navbar-expand-lg sticky-top">
            <div className="container">
                <Link className="navbar-brand" to="/">
				<div className="logo-container">
                        <div className="circle">
                            <span className="logo-text">M</span>
                        </div>
                        <span className="logo-text">Mudemy</span>
                </div>
				</Link>
                <button 
                className="navbar-toggler"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#navbarNav"
                >
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav ms-auto">
                        <li className="nav-item">
                            <Link className="nav-link" to="/">Home</Link>
                        </li>
                        <li className="nav-item">
						<Link className="nav-link" to="/cart">
						<img src={require('../images/cartt.png')}  alt="Cart" style={{ width: '30px', height: '30px', marginRight: '5px' }} />
							Cart
						</Link>
                        </li>
                    
                        {user ? (
                            <>
                            <li className="nav-item">
                                <Link className="nav-link" to="/profile">Profile</Link>
                            </li>
                            <li className="nav-item">
                                <button className="btn btn-danger" onClick={logout}>Logout</button>
                            </li>
                            </>
                        ) :(
                            <li className="nav-item">
                                <Link className="nav-link" to="/login">Login</Link>
                            </li>
                        )}
                    </ul>
                </div>
            </div>
        </nav>
    );
}