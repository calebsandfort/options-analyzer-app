﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using OptionsAnalyzerApp.Data;
using OptionsAnalyzerApp.Models;
using System.Collections.ObjectModel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OptionsAnalyzerApp.Controllers
{
    [Route("api/[controller]")]
    public class GenerationController : Controller
    {
        private readonly OptionsAnalyzerContext _context;

        public GenerationController(OptionsAnalyzerContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet]
        [Route("BlackScholesLookup")]
        public String GetBlackScholesLookup()
        {
            StringBuilder sb = new StringBuilder();

            #region CsvContents
            String csvContents = @"-2.9,.0019,.0018,.0018,.0017,.0017,.0016,.0015,.0015,.0014,.0014
-2.8,.0026,.0025,.0024,.0023,.0023,.0022,.0021,.0021,.0020,.0019
-2.7,.0035,.0034,.0033,.0032,.0031,.0030,.0029,.0028,.0027,.0026
-2.6,.0047,.0045,.0044,.0043,.0041,.0040,.0039,.0038,.0037,.0036
-2.5,.0062,.0060,.0059,.0057,.0055,.0054,.0052,.0051,.0049,.0048
-2.4,.0082,.0080,.0078,.0075,.0073,.0071,.0069,.0068,.0066,.0064
-2.3,.0107,.0104,.0102,.0099,.0096,.0094,.0091,.0089,.0087,.0084
-2.2,.0139,.0136,.0132,.0129,.0125,.0122,.0119,.0116,.0113,.0110
-2.1,.0179,.0174,.0170,.0166,.0162,.0158,.0154,.0150,.0146,.0143
-2.0,.0228,.0222,.0217,.0212,.0207,.0202,.0197,.0192,.0188,.0183
-1.9,.0287,.0281,.0275,.0268,.0262,.0256,.0250,.0244,.0239,.0233
-1.8,.0359,.0351,.0344,.0336,.0329,.0322,.0314,.0307,.0300,.0294
-1.7,.0446,.0436,.0427,.0418,.0409,.0401,.0392,.0384,.0375,.0367
-1.6,.0548,.0537,.0526,.0516,.0505,.0495,.0485,.0475,.0465,.0455
-1.5,.0668,.0655,.0643,.0630,.0618,.0606,.0594,.0582,.0571,.0560
-1.4,.0808,.0793,.0778,.0764,.0750,.0735,.0721,.0708,.0694,.0681
-1.3,.0968,.0951,.0934,.0918,.0901,.0885,.0869,.0853,.0838,.0823
-1.2,.1151,.1131,.1112,.1093,.1075,.1056,.1038,.1020,.1003,.0985
-1.1,.1357,.1335,.1314,.1292,.1271,.1251,.1230,.1210,.1190,.1170
-1.0,.1587,.1562,.1539,.1515,.1492,.1469,.1446,.1423,.1401,.1379
-0.9,.1841,.1814,.1788,.1762,.1736,.1711,.1685,.1660,.1635,.1611
-0.8,.2119,.2090,.2061,.2033,.2005,.1977,.1949,.1921,.1894,.1867
-0.7,.2420,.2389,.2358,.2327,.2296,.2266,.2236,.2206,.2177,.2148
-0.6,.2743,.2709,.2676,.2643,.2611,.2578,.2546,.2514,.2483,.2451
-0.5,.3085,.3050,.3015,.2981,.2946,.2912,.2877,.2843,.2810,.2776
-0.4,.3446,.3400,.3372,.3336,.3300,.3264,.3228,.3192,.3156,.3121
-0.3,.3821,.3783,.3745,.3707,.3669,.3632,.3594,.3557,.3520,.3483
-0.2,.4207,.4168,.4129,.4090,.4052,.4013,.3974,.3936,.3897,.3859
-0.1,.4602,.4562,.4522,.4483,.4443,.4404,.4364,.4325,.4286,.4247
-0.0,.5000,.4960,.4920,.4880,.4840,.4801,.4761,.4721,.4681,.4641
0.0,.5000,.5040,.5080,.5120,.5160,.5199,.5239,.5279,.5319,.5359
0.1,.5398,.5438,.5478,.5517,.5557,.5596,.5636,.5675,.5714,.5753
0.2,.5793,.5832,.5871,.5910,.5948,.5987,.6026,.6064,.6103,.6141
0.3,.6179,.6217,.6255,.6293,.6331,.6368,.6406,.6443,.6480,.6517
0.4,.6554,.6592,.6628,.6664,.6700,.6736,.6772,.6808,.6844,.6880
0.5,.6915,.6950,.6985,.7019,.7054,.7088,.7123,.7157,.7190,.7224
0.6,.7257,.7291,.7324,.7357,.7389,.7422,.7454,.7486,.7517,.7549
0.7,.7580,.7611,.7642,.7673,.7704,.7734,.7764,.7794,.7823,.7852
0.8,.7881,.7910,.7939,.7967,.7995,.8023,.8051,.8078,.8106,.8133
0.9,.8159,.8186,.8212,.8238,.8264,.8289,.8315,.8340,.8365,.8389
1.0,.8413,.8438,.8461,.8485,.8508,.8531,.8554,.8577,.8599,.8621
1.1,.8643,.8665,.8686,.8708,.8729,.8749,.8770,.8790,.8810,.8830
1.2,.8849,.8870,.8888,.8907,.8925,.8944,.8962,.8980,.8997,.9015
1.3,.9032,.9049,.9066,.9082,.9099,.9115,.9131,.9147,.9162,.9177
1.4,.9192,.9207,.9222,.9236,.9251,.9265,.9279,.9292,.9306,.9319
1.5,.9332,.9345,.9357,.9370,.9382,.9394,.9406,.9418,.9429,.9441
1.6,.9452,.9463,.9474,.9484,.9495,.9505,.9515,.9525,.9535,.9545
1.7,.9554,.9564,.9573,.9582,.9591,.9599,.9608,.9616,.9625,.9633
1.8,.9641,.9649,.9656,.9664,.9671,.9678,.9686,.9693,.9700,.9706
1.9,.9713,.9719,.9726,.9732,.9738,.9744,.9750,.9756,.9761,.9767
2.0,.9772,.9778,.9783,.9788,.9793,.9798,.9803,.9808,.9812,.9817
2.1,.9821,.9826,.9830,.9834,.9838,.9842,.9846,.9850,.9854,.9857
2.2,.9861,.9864,.9868,.9871,.9875,.9878,.9881,.9884,.9887,.9890
2.3,.9893,.9896,.9898,.9901,.9904,.9906,.9909,.9911,.9913,.9916
2.4,.9918,.9920,.9922,.9925,.9927,.9929,.9931,.9932,.9934,.9936
2.5,.9938,.9940,.9941,.9943,.9945,.9946,.9948,.9949,.9951,.9952
2.6,.9953,.9955,.9956,.9957,.9959,.9960,.9961,.9962,.9963,.9964
2.7,.9965,.9966,.9967,.9968,.9969,.9970,.9971,.9972,.9973,.9974
2.8,.9974,.9975,.9976,.9977,.9977,.9978,.9979,.9979,.9980,.9981
2.9,.9981,.9982,.9982,.9983,.9984,.9984,.9985,.9985,.9986,.9987
3.0,.9987,1,1,1,1,1,1,1,1,1";
            #endregion

            sb.AppendLine("Dictionary<String, List<Double>> _blackScholesLookup = new Dictionary<String, List<Double>>();");

            using (TextReader tr = new StringReader(csvContents))
            {
                var csv = new CsvReader(tr, new CsvConfiguration() { HasHeaderRecord = false });
                while (csv.Read())
                {
                    sb.AppendLine(String.Format("_blackScholesLookup.Add(\"{10}\", new List<Double>(){{ {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9} }});",
                        csv.GetField<Decimal>(1),
                        csv.GetField<Decimal>(2),
                        csv.GetField<Decimal>(3),
                        csv.GetField<Decimal>(4),
                        csv.GetField<Decimal>(5),
                        csv.GetField<Decimal>(6),
                        csv.GetField<Decimal>(7),
                        csv.GetField<Decimal>(8),
                        csv.GetField<Decimal>(9),
                        csv.GetField<Decimal>(10),
                        csv.GetField<String>(0)));
                }
            }

            return sb.ToString();
            //return String.Format("{0:N2}", 0.3);
        }

        [HttpGet]
        [Route("UpdateOptions")]
        public void GetUpdateOptions(DateTime newExpiry, Decimal newUnderlyingPrice)
        {
            TradingAccount tradingAccount = _context.TradingAccounts.First();
            Decimal spread;

            foreach (Option option in _context.Options.ToList())
            {
                option.Expiry = newExpiry;
                option.UnderlyingPrice = newUnderlyingPrice;
                option.BlackScholesPrice = option.CalculateBlackScholesPrice(tradingAccount, newUnderlyingPrice);

                spread = option.Spread;
                option.Bid = option.BlackScholesPrice - (spread / 2m);
                option.Ask = option.BlackScholesPrice + (spread / 2m);
                option.FillCalculatedFields(tradingAccount);
            }

            _context.SaveChanges();
        }

        [HttpGet]
        [Route("Now")]
        public String GetNow()
        {
            var myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            return TimeZoneInfo.ConvertTime(DateTime.UtcNow, myTimeZone).ToString();
        }

        [HttpGet]
        [Route("Timezones")]
        public String GetTimezones()
        {
            ReadOnlyCollection<TimeZoneInfo> tz;
            tz = TimeZoneInfo.GetSystemTimeZones();

            return String.Join(Environment.NewLine, tz.Select(x => x.Id).OrderBy(s => s));
        }
    }
}
