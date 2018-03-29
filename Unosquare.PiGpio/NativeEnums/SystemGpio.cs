namespace Unosquare.PiGpio.NativeEnums
{
    /// <summary>
    /// Provides an enumeration of System GPIOs from 0 to 53.
    /// User GPIOs are from 0 to 31 (some of them are reserved depending on hardware).
    /// All chip GPIOs go from 0 to 53. It is safe to read all of them but writing to some
    /// of them might crash the hardware and corrupt the SD card. So use documetned ones only.
    /// </summary>
    public enum SystemGpio : int
    {
        /// <summary>The BCM GPIO 00</summary>
        Bcm00 = 0,

        /// <summary>The BCM GPIO 01</summary>
        Bcm01 = 1,

        /// <summary>The BCM GPIO 02</summary>
        Bcm02 = 2,

        /// <summary>The BCM GPIO 03</summary>
        Bcm03 = 3,

        /// <summary>The BCM GPIO 04</summary>
        Bcm04 = 4,

        /// <summary>The BCM GPIO 05</summary>
        Bcm05 = 5,

        /// <summary>The BCM GPIO 06</summary>
        Bcm06 = 6,

        /// <summary>The BCM GPIO 07</summary>
        Bcm07 = 7,

        /// <summary>The BCM GPIO 08</summary>
        Bcm08 = 8,

        /// <summary>The BCM GPIO 09</summary>
        Bcm09 = 9,

        /// <summary>The BCM GPIO 10</summary>
        Bcm10 = 10,

        /// <summary>The BCM GPIO 11</summary>
        Bcm11 = 11,

        /// <summary>The BCM GPIO 12</summary>
        Bcm12 = 12,

        /// <summary>The BCM GPIO 13</summary>
        Bcm13 = 13,

        /// <summary>The BCM GPIO 14</summary>
        Bcm14 = 14,

        /// <summary>The BCM GPIO 15</summary>
        Bcm15 = 15,

        /// <summary>The BCM GPIO 16</summary>
        Bcm16 = 16,

        /// <summary>The BCM GPIO 17</summary>
        Bcm17 = 17,

        /// <summary>The BCM GPIO 18</summary>
        Bcm18 = 18,

        /// <summary>The BCM GPIO 19</summary>
        Bcm19 = 19,

        /// <summary>The BCM GPIO 20</summary>
        Bcm20 = 20,

        /// <summary>The BCM GPIO 21</summary>
        Bcm21 = 21,

        /// <summary>The BCM GPIO 22</summary>
        Bcm22 = 22,

        /// <summary>The BCM GPIO 23</summary>
        Bcm23 = 23,

        /// <summary>The BCM GPIO 24</summary>
        Bcm24 = 24,

        /// <summary>The BCM GPIO 25</summary>
        Bcm25 = 25,

        /// <summary>The BCM GPIO 26</summary>
        Bcm26 = 26,

        /// <summary>The BCM GPIO 27</summary>
        Bcm27 = 27,

        /// <summary>The BCM GPIO 28</summary>
        Bcm28 = 28,

        /// <summary>The BCM GPIO 29</summary>
        Bcm29 = 29,

        /// <summary>The BCM GPIO 30</summary>
        Bcm30 = 30,

        /// <summary>The BCM GPIO 31</summary>
        Bcm31 = 31,

        /// <summary>The BCM GPIO 32</summary>
        Bcm32 = 32,

        /// <summary>The BCM GPIO 33</summary>
        Bcm33 = 33,

        /// <summary>The BCM GPIO 34</summary>
        Bcm34 = 34,

        /// <summary>The BCM GPIO 35</summary>
        Bcm35 = 35,

        /// <summary>The BCM GPIO 36</summary>
        Bcm36 = 36,

        /// <summary>The BCM GPIO 37</summary>
        Bcm37 = 37,

        /// <summary>The BCM GPIO 38</summary>
        Bcm38 = 38,

        /// <summary>The BCM GPIO 39</summary>
        Bcm39 = 39,

        /// <summary>The BCM GPIO 40</summary>
        Bcm40 = 40,

        /// <summary>The BCM GPIO 41</summary>
        Bcm41 = 41,

        /// <summary>The BCM GPIO 42</summary>
        Bcm42 = 42,

        /// <summary>The BCM GPIO 43</summary>
        Bcm43 = 43,

        /// <summary>The BCM GPIO 44</summary>
        Bcm44 = 44,

        /// <summary>The BCM GPIO 45</summary>
        Bcm45 = 45,

        /// <summary>The BCM GPIO 46</summary>
        Bcm46 = 46,

        /// <summary>The BCM GPIO 47</summary>
        Bcm47 = 47,

        /// <summary>The BCM GPIO 48</summary>
        Bcm48 = 48,

        /// <summary>The BCM GPIO 49</summary>
        Bcm49 = 49,

        /// <summary>The BCM GPIO 50</summary>
        Bcm50 = 50,

        /// <summary>The BCM GPIO 51</summary>
        Bcm51 = 51,

        /// <summary>The BCM GPIO 52</summary>
        Bcm52 = 52,

        /// <summary>The BCM GPIO 53</summary>
        Bcm53 = 53,
    }
}
