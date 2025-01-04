import { useAuth } from "../context/AuthContext";

export default function Footer(){

    const { user } = useAuth();
    return(
        <footer className="py-3 mt-5" style={{ backgroundColor: "#EE4E34", color: "white" }}>
            <div className="container text-center">
                <div className="row">
                        <div className="col-md-6">
                            <p className="mb-0">&copy; 2025 Mudemy. Tüm hakları saklıdır.</p>
                        </div>
                        <div className="col-md-6">
                            <ul className="list-inline -mb-0">
                                    <li className="list-inline-item">
                                        <a href="/" className="text-light text-decoration-none">Home</a>
                                    </li>
                                    <li className="list-inline-item">
                                        <a href="/cart" className="text-light text-decoration-none">Cart</a>
                                    </li>
                                    {user && (
                                    <li className="list-inline-item">
                                        <a href="/profile" className="text-light text-decoration-none">Profile</a>
                                    </li>
                                    )}
                            </ul>
                        </div>
                </div>
            </div>
        </footer>
    );
}