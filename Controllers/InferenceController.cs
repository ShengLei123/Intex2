using System;
using System.Collections.Generic;
using System.Linq;
using Intex2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace Intex2.Controllers
{
    public class InferenceController : Controller
    {
        private InferenceSession _session;

        public InferenceController(InferenceSession session)
        {
            _session = session;
        }

        [HttpGet]
        public IActionResult EnterData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Prediction(Feature data)
        {
            if (ModelState.IsValid)
            {
                var result = _session.Run(new List<NamedOnnxValue>
                    {
                        NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
                    });

                Tensor<float> score = result.First().AsTensor<float>();

                var prediction = new Prediction { PredictedValue = (float)Math.Round(score.First(), 0) };

                result.Dispose();

                return View("Prediction", prediction); 
            }

            return View("EnterData");
        }
    }
}

//var result = _session.Run(new List<NamedOnnxValue>
//            {
//                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
//            });

//Tensor<float> score = result.First().AsTensor<float>();

//var prediction = new Prediction { PredictedValue = (float)Math.Round(score.First(), 0) };

//result.Dispose();

//return View("Prediction", prediction);