import React, { useState } from "react";

import { Button, Form, Spinner } from "react-bootstrap";

const AddNewAssetForm = ({ onNewAssetAdded }) => {
  const [isAddingAsset, setIsAddingAsset] = useState(false);
  const [formData, setFormData] = useState({
      asset: "",
      change: 0.05,
      days: 1,
    }),
    onDataChange = (e) => setFormData(e.traget);

  const addNewAsset = async (e) => {
    e.preventDefault();
    debugger;
    const formData = new FormData(e.target),
      formDataObj = Object.fromEntries(formData.entries());
    console.log(formDataObj);
    setIsAddingAsset(true);
    // var response = await fetch("http://localhost:5005/api/v1/assets", {
    //   method: "POST",
    //   body: JSON.stringify({
    //     Name: ,
    //     Change: ,
    //     End:
    //   }),
    // });
    setIsAddingAsset(false);
    // onNewAssetAdded();
  };

  return isAddingAsset ? (
    <Spinner />
  ) : (
    <>
      <Form onSubmit={addNewAsset}>
        <h2>Add new asset:</h2>
        <Form.Group className="mb-3" controlId="formNewAsset">
          <Form.Label>Select asset</Form.Label>
          <Form.Select>
            <option value="BTCUSD.AGR">BTC</option>
            <option value="ETHUSD.AGR">ETH</option>
            <option value="AAPL.US">Apple</option>
            <option value="MSFT.US">Microsoft</option>
            <option value="TSLA.US">Tesla</option>
          </Form.Select>
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicChange">
          <Form.Label>Change value to monitor (in %)</Form.Label>
          <Form.Control
            type="number"
            step=".01"
            min=".05"
            defaultValue="0.05"
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicChange">
          <Form.Label>Monitor for (in days)</Form.Label>
          <Form.Control type="number" step="1" min="1" defaultValue="1" />
        </Form.Group>
        <Button variant="primary" type="submit" className="me-3">
          Add asset
        </Button>
      </Form>
    </>
  );
};

export default AddNewAssetForm;
