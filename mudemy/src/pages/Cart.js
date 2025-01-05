import React from 'react';
import { useCart } from '../context/CartContext';
import { Link } from 'react-router-dom';
import courseImage from '../images/courseImage.jpg'
import OrderService from '../services/OrderService';

export default function Cart() {
  const { cart, increaseQuantity, decreaseQuantity, removeFromCart, clearCart } = useCart();

  const handleOrder = async () => {
    try {
      for (const item of cart) {
        await OrderService.createOrder(item.id);
      }
      clearCart();
      alert('Order placed successfully!');
    } catch (error) {
      console.error('Order creation failed:', error);
      alert('An error occurred while placing your order.');
    }
  };

  return (
    <div className='container mt-4'>
      <h2 className="text-center mb-4" style={{ fontWeight: 'bold', color: '#EE4E34' }}>
        Your Cart
      </h2>

      {cart.length === 0 ? (
        <div className="text-center">
          <p className="text-muted" style={{ fontSize: '18px' }}>
            Your cart is empty. Start adding items to see them here!
          </p>
        </div>
      ) : (
        <>
          <ul className="list-group">
            {cart.map((item) => (
              <li
                key={item.id}
                className="list-group-item d-flex justify-content-between align-items-center"
                style={{
                  borderRadius: '15px',
                  boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
                  marginBottom: '10px',
                }}
              >
                <div className="d-flex align-items-center">
                  <img
                    src={courseImage} 
                    alt={item.title}
                    style={{
                      width: '80px',
                      height: '80px',
                      objectFit: 'cover',
                      borderRadius: '10px',
                      marginRight: '15px',
                    }}
                  />
                  <div>
                    <h5 style={{ fontWeight: 'bold', marginBottom: '5px' }}>{item.title}</h5>
                    <p className="mb-1" style={{ color: '#666', fontSize: '14px' }}>
                      {item.price}₺ x {item.quantity}
                    </p>
                    <div>
                      <button
                        className="btn btn-outline-primary btn-sm me-2"
                        onClick={() => increaseQuantity(item.id)}
                      >
                        +
                      </button>
                      <button
                        className="btn btn-outline-secondary btn-sm me-2"
                        onClick={() => decreaseQuantity(item.id)}
                      >
                        -
                      </button>
                      <button
                        className="btn btn-outline-danger btn-sm"
                        onClick={() => removeFromCart(item.id)}
                      >
                        Remove
                      </button>
                    </div>
                  </div>
                </div>
                <p className="fw-bold" style={{ color: '#EE4E34' }}>
                  Total: {item.price * item.quantity}₺
                </p>
              </li>
            ))}
          </ul>

          <div className="d-flex justify-content-end mt-4">
            <button
              className="btn btn-danger"
              style={{
                backgroundColor: '#EE4E34',
                border: 'none',
                borderRadius: '25px',
                padding: '10px 20px',
              }}
              onClick={clearCart}
            >
              Clear Cart
            </button>
            <Link
              to="/payment"
              className="btn ms-3"
              style={{
                backgroundColor: '#4CAF50',
                color: '#fff',
                borderRadius: '25px',
                padding: '10px 20px',
              }}
              onClick={handleOrder}
            >
              Proceed to Payment
            </Link>
          </div>
        </>
      )}
    </div>
  );
}
