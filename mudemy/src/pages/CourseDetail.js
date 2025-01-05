import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { useCart } from '../context/CartContext';
import { fetchProductById } from '../services/api';

export default function CourseDetail() {
  const { id } = useParams();

  const [product, setProduct] = useState(null);

  const { addToCart } = useCart();

  useEffect(() => {
    fetchProductById(id).then((res) => setProduct(res.data));
  }, [id]);

  if (!product) return <div className='container mt-4'>Loading...</div>;

  return (
    <div className='container mt-4'>
      <div className='row'>

        <div className='col-md-4'>
          <img
            src={product.image}
            alt={product.title}
            className='img-fluid'
            style={{
              objectFit: 'contain',
              height: '250px',
              width: '100%',
              borderRadius: '10px',
              boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
            }}
          />
        </div>
        
        <div className='col-md-8 d-flex flex-column justify-content-center'>
          <h2 style={{ fontWeight: 'bold', color: '#333' }}>{product.title}</h2>
          <p style={{ fontSize: '14px', color: '#666' }}>{product.description}</p>
          <p>
            <strong>Category: </strong>
            {product.category || 'N/A'}
          </p>
          <p>
            <strong>Added By: </strong>
            {product.addedBy || 'Unknown'}
          </p>
          <h4 style={{ color: '#EE4E34', fontWeight: 'bold' }}>{product.price}₺</h4>
          <button
            className='btn'
            style={{
              backgroundColor: '#EE4E34',
              color: '#fff',
              padding: '10px 20px',
              borderRadius: '25px',
              cursor: 'pointer',
              width: '300px',
            }}
            onClick={() => addToCart(product)}
          >
            Add To Cart
          </button>
        </div>
      </div>
    </div>
  );
}
