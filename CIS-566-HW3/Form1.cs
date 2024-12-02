using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CIS_566_HW3
{
    public partial class Form1 : Form
    {
        LengthConverterClient client;

        public Form1()
        {
            InitializeComponent();
            client = new LengthConverterClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            float km_value = (float) Convert.ToDouble(textBox1.Text);

            float result = client.convert_length(km_value, comboBox1.Text);

            textBox2.Text = result.ToString();
        }
    }


    public class LengthConverterClient
    {
        UnitConverterHandler handler;

        public LengthConverterClient()
        {
            MileConverterHandler mileHandler = new MileConverterHandler();
            YardConverterHandler yardHandler = new YardConverterHandler();

            mileHandler.SetSuccessor(yardHandler);

            FeetConverterHandler feetHandler = new FeetConverterHandler();

            yardHandler.SetSuccessor(feetHandler);

            handler = mileHandler;
        }

        public float convert_length(float value, string target_unit)
        {
            return handler.handle_request(value, target_unit);
        }

    }


    public abstract class UnitConverterHandler
    {
        protected UnitConverterHandler successor;

        public void SetSuccessor(UnitConverterHandler successor)
        {
            this.successor = successor;
        }

        public virtual float handle_request(float value, string target_unit)
        {
            return 0f;
        }
    }


    public class MileConverterHandler : UnitConverterHandler
    {

        public override float handle_request(float value, string target_unit)
        {
            value *= 0.621371f; // convert KM to miles

            if (target_unit == "Mile")
            {
                return value;
            }
            else
            {
                if (successor != null)
                {
                    return successor.handle_request(value, target_unit);
                }
                else
                {
                    return value;
                }
            }
        }

    }


    public class YardConverterHandler : UnitConverterHandler
    {

        public override float handle_request(float value, string target_unit)
        {
            value *= 1760f; // convert miles to yards

            if (target_unit == "Yard")
            {
                return value;
            }
            else
            {
                if (successor != null)
                {
                    return successor.handle_request(value, target_unit);
                }
                else
                {
                    return value;
                }
            }
        }

    }


    public class FeetConverterHandler : UnitConverterHandler
    {

        public override float handle_request(float value, string target_unit)
        {
            value *= 3f; // convert yards to feet

            if (target_unit == "Foot")
            {
                return value;
            }
            else
            {
                if (successor != null)
                {
                    return successor.handle_request(value, target_unit);
                }
                else
                {
                    return value;
                }
            }
        }

    }




}
