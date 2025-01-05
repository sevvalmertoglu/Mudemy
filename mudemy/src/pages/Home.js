import { useEffect, useState } from "react";
import { fetchProduct } from "../services/CourseService";
import CourseCart from "../components/CourseCart";


export default function Home(){

  const [products, setProducts] = useState([]);

  const [filteredProducts, setFilteredProducts] = useState([]);

  const [searhTerm, setSearchTerm] = useState("");

  const [currentPage, setCurrentPage] = useState(1);

  const productsPerPage = 6;

  const [loading, setLoading] = useState(true); 

  useEffect(() => {
    fetchProduct()
      .then((fetchedProducts) => {
        setProducts(fetchedProducts);
        setFilteredProducts(fetchedProducts);
      })
      .catch((error) => {
        console.error("Error fetching products:", error);
      })
      .finally(() => {
        setLoading(false); 
      });
  }, []);

  const handleSearch = (e) => {
    const term = e.target.value;
    setSearchTerm(term);
    filterProducts(term);
    setCurrentPage(1);
  }

  const filterProducts = (term) => {
    let result = products;
    if (term) {
      result = result.filter((product) =>
        product.title.toLowerCase().includes(term.toLowerCase())
      );
    }
    setFilteredProducts(result);
  }

  const indexOfLastProduct = currentPage * productsPerPage;

  const indexOfFirstProduct = indexOfLastProduct - productsPerPage;

  const currentProducts = filteredProducts.slice(indexOfFirstProduct, indexOfLastProduct);

  const totalPages = Math.ceil(filteredProducts.length / productsPerPage);

  const pageNumbers = [...Array(totalPages).keys()].map((n)=> n+1);

  return(
    <div className="container mt-4">
      <h1 className="text-center mb-4" style={{ color:"#EE4E34", fontSize: "30px", fontWeight: "bold"}}>Course List</h1>

      <div className="row mb-3">
        <div className="col-md-6">
          <input
          type="text"
          className="form-control"
          placeholder="Search course.."
          value={searhTerm}
          onChange={handleSearch}
          ></input>
        </div>
      </div>

      <div className="row">
        {currentProducts.map((product)=>(
          <CourseCart key={product.id} product={product}></CourseCart>
        ))}
      </div>

      {totalPages > 1 && (
        <nav>
          <ul className="pagination justifycontent-center mt-4">
            {pageNumbers.map((number) => (
              <li key={number} className={`page-item ${currentPage === number ? "active" : ""}`}>
              <button onClick={()=> setCurrentPage(number)} className="page-link"
                style={{ backgroundColor: "#EE4E34", color: "#fff" }}
                >
                {number}
              </button>
              </li>           
            ))}
          </ul>
        </nav>
      )}
    </div>
  );
}