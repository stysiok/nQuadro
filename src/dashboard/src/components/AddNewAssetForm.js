import React, { useState } from "react";

import { Button, Form, Spinner } from "react-bootstrap";

const AddNewAssetForm = ({ onNewAssetAdded }) => {
  const [isAddingAsset, setIsAddingAsset] = useState(false);
  const [formData, setFormData] = useState({
    asset: "BTCUSD.AGR",
    change: 0.05,
    days: 1,
  });

  const addNewAsset = async (e) => {
    e.preventDefault();
    setIsAddingAsset(true);
    let headers = new Headers();
    headers.set("Content-Type", "application/json");
    let monitorUntil = new Date(
      new Date(
        new Date().setDate(new Date().getDate() + formData.days)
      ).setHours(0, 0, 0)
    );
    var response = await fetch("http://localhost:5005/api/v1/assets", {
      method: "POST",
      headers: headers,
      body: JSON.stringify({
        Name: formData.asset,
        Change: formData.change,
        End: monitorUntil,
      }),
    });
    setIsAddingAsset(false);
    onNewAssetAdded();
  };

  return isAddingAsset ? (
    <Spinner />
  ) : (
    <>
      <Form onSubmit={addNewAsset}>
        <h2>Add new asset:</h2>
        <Form.Group className="mb-3" controlId="formNewAsset">
          <Form.Label>Select asset</Form.Label>
          <Form.Select
            value={formData.asset}
            onChange={(e) =>
              setFormData({ ...formData, asset: e.target.value })
            }
          >
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
            value={formData.change}
            onChange={(e) =>
              setFormData({ ...formData, change: e.target.value })
            }
          />
        </Form.Group>
        <Form.Group className="mb-3" controlId="formBasicMonitorFor">
          <Form.Label>Monitor for (in days)</Form.Label>
          <Form.Control
            type="number"
            step="1"
            min="1"
            value={formData.days}
            onChange={(e) => setFormData({ ...formData, days: e.target.value })}
          />
        </Form.Group>
        <Button variant="primary" type="submit" className="me-3">
          Add asset
        </Button>
      </Form>
    </>
  );
};

export default AddNewAssetForm;
