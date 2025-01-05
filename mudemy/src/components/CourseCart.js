import React from 'react';
import { useCart } from '../context/CartContext';
import { Link } from 'react-router-dom';

export default function CourseCart({ product }) {
  const { addToCart } = useCart();

  return (
    <div className='col-md-4 mb-4'>
      <div className='card h-100 shadow-lg d-flex flex-row align-items-center' style={{ borderRadius: "20px" }}>
        <Link to={`/product/${product.id}`} style={{ textDecoration: "none", flex: "1" }}>
          <img
            src={product.image}
            alt={product.title}
            className='card-img-top'
            style={{ objectFit: "contain", height: "150px", width: "150px" }}
          ></img>
        </Link>
        <div className='card-body d-flex flex-column justify-content-between' style={{ flex: "2" }}>
          <p className='card-title' style={{ fontWeight: "bold", fontSize: "16px" }}>{product.title}</p>
          <p className='card-text' style={{ marginBottom: "10px" }}>{product.price}₺</p>
          <button
            className='btn align-self-end'
            style={{ backgroundColor: "#EE4E34", color: "#fff", padding: "10px 20px", borderRadius: "25px", cursor: "pointer", width: "180px" }}
            onClick={() => addToCart(product)}
          >
            Add to Cart
          </button>
        </div>
      </div>
    </div>
  );
}
